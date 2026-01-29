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
        public async Task<ActionResult<List<EducationDto>>> GetAll()
        {
            var educations = await _context.Education
                .Select(e => new EducationDto
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToListAsync();

            return Ok(educations);
        }

        // GET: api/education/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationDto>> GetById(int id)
        {
            var education = await _context.Education
                .Where(e => e.Id == id)
                .Select(e => new EducationDto
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
        public async Task<ActionResult<EducationDto>> Create(EducationCreateDto dto)
        {
            var education = new Education
            {
                Name = dto.Name
            };

            _context.Education.Add(education);
            await _context.SaveChangesAsync();

            var result = new EducationDto
            {
                Id = education.Id,
                Name = education.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/education/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EducationUpdateDto dto)
        {
            var education = await _context.Education.FindAsync(id);
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
            var education = await _context.Education.FindAsync(id);
            if (education == null)
                return NotFound();

            _context.Education.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
