namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationalStandardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EducationalStandardsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EducationalStandardsDto>>> GetAll()
        {
            return Ok(await _context.EducationalStandards
                .Select(es => new EducationalStandardsDto
                {
                    Id = es.Id,
                    Title = es.title,
                    Description = es.Description,
                    Number = es.Number,
                    Education = new GetEducationDto
                    {
                        Id = es.Education.Id,
                        Name = es.Education.Name
                    },

                })
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EducationalStandardsDto>> GetById(int id)
        {
            var item = await _context.EducationalStandards
                .Where(es => e.Id == id)
                .Select(es => new EducationalStandardsDto
                {
                    Id = es.Id,
                    Title = es.title,
                    Description = es.Description,
                    Number = es.Number,
                    Education = new GetEducationDto
                    {
                        Id = es.Education.Id,
                        Name = es.Education.Name
                    },
                })
                .FirstOrDefaultAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<EducationalStandardsDto>> Create(EducationalStandardsCreateDto dto)
        {
            if (!await _context.Educations.AnyAsync(e => e.Id == dto.EducationId))
                return BadRequest("Education does not exist");

            var entity = new EducationalStandards
            {
                Title = dto.title,
                Description = dto.Description,
                Number = dto.Number,
                educationId = dto.educationId
            };

            _context.EducationalStandards.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new EducationalStandardsDto
            {
                Id = entity.Id,
                Title = entity.title,
                Description = entity.Description,
                Number = entity.Number,
                educationId = entity.educationId
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EducationalStandardsUpdateDto dto)
        {
            var entity = await _context.EducationalStandards.FindAsync(id);
            if (entity == null)
                return NotFound();

            if (!await _context.Educations.AnyAsync(e => e.Id == dto.EducationId))
                return BadRequest("Education does not exist");

            entity.Title = dto.title;
            entity.Description = dto.Description;
            entity.Number = dto.Number;
            entity.educationId = dto.educationId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.EducationalStandards.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.EducationalStandards.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
