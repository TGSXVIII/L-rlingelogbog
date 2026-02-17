namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TasksDto>>> GetAll()
        {
            return Ok(await _context.Tasks
                .Select(t => new TasksDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Start_Date = t.Start_Date,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    educationStandartsDTO = new GetEducationalStandartsDTO
                    {
                        Id = t.educationStandartsDTO.id,
                        Title = t.educationStandartsDTO.Title,
                        Description = t.educationStandartsDTO.Description,
                        Number = t.educationStandartsDTO.Number,
                        educationDTO = new GetEducationDTO
                        {
                            Id = t.educationStandartsDTO.educationDTO.id,
                            Name = t.educationStandartsDTO.educationDTO.Name,
                        }
                    },
                    assignedToDTO = new GetUserDTO
                    {
                        Id = t.assignedToDTO.id,
                        Name = t.assignedToDTO.Name,
                        Email = t.assignedToDTO.Email,
                        Role = t.assignedToDTO.Role,
                    },
                    createdByDTO = new GetUserDTO
                    {
                        Id = t.createdByDTO.id,
                        Name = t.createdByDTO.Name,
                        Email = t.createdByDTO.Email,
                        Role = t.createdByDTO.Role,
                    },
                })
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TasksDto>> GetById(int id)
        {
            var task = await _context.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TasksDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Start_Date = t.Start_Date,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    educationStandartsDTO = new GetEducationalStandartsDTO
                    {
                        Id = t.educationStandartsDTO.id,
                        Title = t.educationStandartsDTO.Title,
                        Description = t.educationStandartsDTO.Description,
                        Number = t.educationStandartsDTO.Number,
                        educationDTO = new GetEducationDTO
                        {
                            Id = t.educationStandartsDTO.educationDTO.id,
                            Name = t.educationStandartsDTO.educationDTO.Name,
                        }
                    },
                    assignedToDTO = new GetUserDTO
                    {
                        Id = t.assignedToDTO.id,
                        Name = t.assignedToDTO.Name,
                        Email = t.assignedToDTO.Email,
                        Role = t.assignedToDTO.Role,
                    },
                    createdByDTO = new GetUserDTO
                    {
                        Id = t.createdByDTO.id,
                        Name = t.createdByDTO.Name,
                        Email = t.createdByDTO.Email,
                        Role = t.createdByDTO.Role,
                    },
                })
                .FirstOrDefaultAsync();

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet]
        public async Task<ActionResult<List<TasksDto>>> GetAllWaiting([FromQuery] int userId)
        {
            return Ok(await _context.Tasks
                .Where(t => t.createdByDTO.id == userId
                            && t.Status == "waitingForReview")
                .Select(t => new TasksDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Start_Date = t.Start_Date,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    educationStandartsDTO = new GetEducationalStandartsDTO
                    {
                        Id = t.educationStandartsDTO.id,
                        Title = t.educationStandartsDTO.Title,
                        Description = t.educationStandartsDTO.Description,
                        Number = t.educationStandartsDTO.Number,
                        educationDTO = new GetEducationDTO
                        {
                            Id = t.educationStandartsDTO.educationDTO.id,
                            Name = t.educationStandartsDTO.educationDTO.Name,
                        }
                    },
                    assignedToDTO = new GetUserDTO
                    {
                        Id = t.assignedToDTO.id,
                        Name = t.assignedToDTO.Name,
                        Email = t.assignedToDTO.Email,
                        Role = t.assignedToDTO.Role,
                    },
                    createdByDTO = new GetUserDTO
                    {
                        Id = t.createdByDTO.id,
                        Name = t.createdByDTO.Name,
                        Email = t.createdByDTO.Email,
                        Role = t.createdByDTO.Role,
                    },
                })
                .ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<TasksDto>> Create(TasksCreateDto dto)
        {
            var educationStandard = await _context.EducationalStandards.FindAsync(dto.educationStandartsId);
            var assignedUser = await _context.Users.FindAsync(dto.assignedTo);
            var createdByUser = await _context.Users.FindAsync(dto.createdBy);

            if (educationStandard == null || assignedUser == null || createdByUser == null)
                return BadRequest("One or more referenced entities do not exist.");

            var entity = new Tasks
            {
                Title = dto.Title,
                Description = dto.Description,
                Start_Date = dto.Start_Date,
                DueDate = dto.DueDate,
                Status = Tasks.Status.Pending,
                educationStandartsId = dto.educationStandartsId,
                assignedTo = dto.assignedTo,
                createdBy = dto.createdBy,
            };

            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new TasksDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Start_Date = entity.Start_Date,
                DueDate = entity.DueDate,
                Status = Tasks.Status.Pending,
                educationStandartsId = entity.educationStandartsId,
                assignedTo = entity.assignedTo,
                createdBy = entity.createdBy,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TasksUpdateDto dto)
        {
            var educationStandard = await _context.EducationalStandards.FindAsync(dto.educationStandartsId);
            var assignedUser = await _context.Users.FindAsync(dto.assignedTo);
            var createdByUser = await _context.Users.FindAsync(dto.createdBy);

            if (educationStandard == null || assignedUser == null || createdByUser == null)
                return BadRequest("One or more referenced entities do not exist.");

            var entity = await _context.Tasks.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Start_Date = dto.Start_Date;
            entity.DueDate = dto.DueDate;
            entity.Status = dto.Status;
            entity.educationStandartsId = dto.educationStandartsId;
            entity.assignedTo = dto.assignedTo;
            entity.createdBy = dto.createdBy;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Tasks.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
