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
                        Name = ue.GetUserDTO.Name

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
                    // TODO: map foreign keys & other fields
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
                // TODO: assign UserId, EducationId, etc.
            };

            _context.Users_Education.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new GetUsers_EducationDTO
            {
                Id = entity.Id,
                // TODO: map properties
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUsers_EducationDTO dto)
        {
            var entity = await _context.Users_Education.FindAsync(id);
            if (entity == null)
                return NotFound();

            // TODO: update properties

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