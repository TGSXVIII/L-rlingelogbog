namespace API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, PasswordService passwordService, JwtService jwtService)
        {
            _context = context;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        // =========================
        // Register
        // =========================
        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password, string email)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
                return BadRequest("User already exists");

            var user = new Users
            {
                Username = username,
                Email = email
            };

            // Use the HashPassword overload that accepts a single string (password).
            user.PasswordHash = _passwordService.HashPassword(password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }
        // =========================
        // Login
        // =========================
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null) return Unauthorized("Invalid username or password");

            // VerifyPassword expects the stored hash (string) and the provided password (string)
            if (!_passwordService.VerifyPassword(user.PasswordHash, password))
                return Unauthorized("Invalid username or password");

            var accessToken = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token to DB
            var dbToken = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(dbToken);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }

        // =========================
        // Refresh Access Token
        // =========================
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == refreshToken);

            if (token == null || token.IsRevoked || token.Expires < DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token");

            var newAccessToken = _jwtService.GenerateToken(token.User);

            return Ok(new { accessToken = newAccessToken });
        }

        // =========================
        // Get Current User Info
        // =========================
        [HttpGet("me")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Me()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound();

            return Ok(new
            {
                user.Username,
                Role = user.Role.ToString()
            });
        }

        // =========================
        // Admin Only Endpoint
        // =========================
        [HttpGet("admin")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return Ok("You are an admin");
        }
    }
}
