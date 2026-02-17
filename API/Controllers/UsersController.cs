using System.Runtime.Serialization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos = users.Select(u => new GetUserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();
            return Ok(userDtos);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
            return Ok(userDto);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<GetUserDTO>> Create(CreateUserDTO dto)
        {
            var user = new Users
            {
                Name = dto.Name,
                Username = dto.Username,
                PasswordHash = dto.PasswordHash,
                Email = dto.Email,
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role =  user.Role
            };

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, result);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}