using API.DTO;

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
        public async Task<ActionResult<List<GetEducationalStandartsDTO>>> GetAll()
        {
            return Ok(await _context.EducationalStandarts
                .Select(es => new GetEducationalStandartsDTO
                {
                    Id = es.Id,
                    Title = es.Title,
                    Description = es.Description,
                    Number = es.Number,
                    educationDTO = new GetEducationDTO
                    {
                        Id = es.Education.Id,
                        Name = es.Education.Name
                    }
                })
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEducationalStandartsDTO>> GetById(int id)
        {
            var item = await _context.EducationalStandarts
                .Where(es => es.Id == id)
                .Select(es => new GetEducationalStandartsDTO
                {
                    Id = es.Id,
                    Title = es.Title,
                    Description = es.Description,
                    Number = es.Number,
                    educationDTO = new GetEducationDTO
                    {
                        Id = es.Education.Id,
                        Name = es.Education.Name
                    }
                })
                .FirstOrDefaultAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetEducationalStandartsDTO>> Create(CreateEducationalStandartsDTO dto)
        {
            if (!await _context.Educations.AnyAsync(e => e.Id == dto.educationId))
                return BadRequest("Education does not exist");

            var entity = new EducationalStandarts
            {
                Title = dto.Title,
                Description = dto.Description,
                Number = dto.Number,
                EducationId = dto.educationId
            };

            _context.EducationalStandarts.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new GetEducationalStandartsDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Number = entity.Number,
                educationDTO = new GetEducationDTO
                {
                    Id = dto.educationId,
                    Name = (await _context.Educations.FindAsync(dto.educationId)).Name
                }
            });
        }

        [HttpPut("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateEducationalStandartsDTO dto)
        {
            var entity = await _context.EducationalStandarts.FindAsync(id);
            if (entity == null)
                return NotFound();

            if (!await _context.Educations.AnyAsync(e => e.Id == dto.educationId))
                return BadRequest("Education does not exist");

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Number = dto.Number;
            entity.EducationId = dto.educationId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.EducationalStandarts.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.EducationalStandarts.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
