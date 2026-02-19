using API.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EducationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/education
        [HttpGet]
        public async Task<ActionResult<List<GetEducationDTO>>> GetAll()
        {
            var educations = await _context.Educations
                .Select(e => new GetEducationDTO
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToListAsync();

            return Ok(educations);
        }

        // GET: api/education/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEducationDTO>> GetById(int id)
        {
            var education = await _context.Educations
                .Where(e => e.Id == id)
                .Select(e => new GetEducationDTO
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .FirstOrDefaultAsync();

            if (education == null)
                return NotFound();

            return Ok(education);
        }

        // POST: api/education
        [HttpPost]
        public async Task<ActionResult<GetEducationDTO>> Create(CreateEducationDTO dto)
        {
            var education = new Education
            {
                Name = dto.Name
            };

            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            var result = new GetEducationDTO
            {
                Id = education.Id,
                Name = education.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/education/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEducationDTO dto)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
                return NotFound();

            education.Name = dto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/education/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
                return NotFound();

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
