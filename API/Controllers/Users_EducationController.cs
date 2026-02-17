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
                        Id = ue.GetUserDTO.Id,
                        Name = ue.GetUserDTO.Name,
                        Email = ue.GetUserDTO.Email,
                        Role = ue.GetUserDTO.Role
                    },
                    educationDTO = new GetEducationDTO
                    {
                        Id = ue.EducationDTO.Id,
                        Degree = ue.EducationDTO.Degree,
                        Institution = ue.EducationDTO.Institution,
                        YearOfCompletion = ue.EducationDTO.YearOfCompletion
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
                        Id = ue.GetUserDTO.Id,
                        Name = ue.GetUserDTO.Name,
                        Email = ue.GetUserDTO.Email,
                        Role = ue.GetUserDTO.Role
                    },
                    educationDTO = new GetEducationDTO
                    {
                        Id = ue.EducationDTO.Id,
                        Degree = ue.EducationDTO.Degree,
                        Institution = ue.EducationDTO.Institution,
                        YearOfCompletion = ue.EducationDTO.YearOfCompletion
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
                    Id = entity.GetUserDTO.Id,
                    Name = entity.GetUserDTO.Name,
                    Email = entity.GetUserDTO.Email,
                    Role = entity.GetUserDTO.Role
                },
                educationDTO = new GetEducationDTO
                {
                    Id = entity.EducationDTO.Id,
                    Degree = entity.EducationDTO.Degree,
                    Institution = entity.EducationDTO.Institution,
                    YearOfCompletion = entity.EducationDTO.YearOfCompletion
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
