namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersEducationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersEducationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetUsers_EducationDTO>>> GetAll()
        {
            return Ok(await _context.Users_Education
                .Select(ue => new GetUsers_EducationDTO
                {
                    Id = ue.Id,
                    Grade = ue.Grade,
                    UserDTO = new GetUserDTO
                    {
                        Id = ue.Id,
                        Name = ue.User.Name,
                        Email = ue.User.Email,
                        Role = ue.User.Role
                    },
                    educationDTO = new GetEducationDTO
                    {
                        Id = ue.Education.Id,
                        Name = ue.Education.Name
                    }

                })
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUsers_EducationDTO>> GetById(int id)
        {
            var item = await _context.Users_Education
                .Where(ue => ue.Id == id)
                .Select(ue => new GetUsers_EducationDTO
                {
                    Id = ue.Id,
                    Grade = ue.Grade,
                    UserDTO = new GetUserDTO
                    {
                        Id = ue.User.Id,
                        Name = ue.User.Name,
                        Email = ue.User.Email,
                        Role = ue.User.Role
                    },
                    educationDTO = new GetEducationDTO
                    {
                        Id = ue.Education.Id,
                        Name = ue.Education.Name
                    }

                })
                .FirstOrDefaultAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<GetUsers_EducationDTO>> Create(CreateUsers_EducationDTO dto)
        {
            var entity = new Users_Education
            {
                Grade = "N/A",
                UserId = dto.UserId,
                EducationId = dto.EducationId
            };

            _context.Users_Education.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new GetUsers_EducationDTO
            {
                Id = entity.Id,
                Grade = entity.Grade,
                UserDTO = new GetUserDTO
                {
                    Id = entity.User.Id,
                    Name = entity.User.Name,
                    Email = entity.User.Email,
                    Role = entity.User.Role
                },
                educationDTO = new GetEducationDTO
                {
                    Id = entity.Education.Id,
                    Name = entity.Education.Name
                }
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateUsers_EducationDTO dto)
        {
            var entity = await _context.Users_Education
                .Where(ue => ue.UserId == dto.UserId && ue.EducationId == dto.EducationId)
                .FirstOrDefaultAsync();
            if (entity == null)
                return NotFound();

            entity.Grade = dto.Grade;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Users_Education.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.Users_Education.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
