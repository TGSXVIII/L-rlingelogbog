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
        public async Task<ActionResult<List<GetTaskDTO>>> GetAll()
        {
            return Ok(await _context.Tasks
                .Select(t => new GetTaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Start_Date = t.Start_Date,
                    DueDate = t.DueDate,
                    TaskStatus = t.TaskStatus,
                    assignedToDTO = new GetUserDTO
                    {
                        Id = t.assignedToId,
                        Name = t.assignedTo.Name,
                        Email = t.assignedTo.Email,
                        Role = t.assignedTo.Role,
                    },
                    createdByDTO = new GetUserDTO
                    {
                        Id = t.createdById,
                        Name = t.createdBy.Name,
                        Email = t.createdBy.Email,
                        Role = t.createdBy.Role,
                    },
                    EducationalStandarts = t.Tasks_EducationalStandarts
                    .Select(te => new GetEducationalStandartsDTO
                    {
                        Id = te.EducationalStandart.Id,
                        Title = te.EducationalStandart.Title,
                        Description = te.EducationalStandart.Description,
                        Number = te.EducationalStandart.Number,
                        educationDTO = new GetEducationDTO
                        {
                            Id = te.EducationalStandart.Education.Id,
                            Name = te.EducationalStandart.Education.Name
                        }
                    })
                    .ToList()
                })
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaskDTO>> GetById(int id)
        {
            var task = await _context.Tasks
                .Where(t => t.Id == id)
                .Select(t => new GetTaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Start_Date = t.Start_Date,
                    DueDate = t.DueDate,
                    TaskStatus = t.TaskStatus,
                    assignedToDTO = new GetUserDTO
                    {
                        Id = t.assignedToId,
                        Name = t.assignedTo.Name,
                        Email = t.assignedTo.Email,
                        Role = t.assignedTo.Role,
                    },
                    createdByDTO = new GetUserDTO
                    {
                        Id = t.createdById,
                        Name = t.createdBy.Name,
                        Email = t.createdBy.Email,
                        Role = t.createdBy.Role,
                    },
                    EducationalStandarts = t.Tasks_EducationalStandarts
                    .Select(te => new GetEducationalStandartsDTO
                    {
                        Id = te.EducationalStandart.Id,
                        Title = te.EducationalStandart.Title,
                        Description = te.EducationalStandart.Description,
                        Number = te.EducationalStandart.Number,
                        educationDTO = new GetEducationDTO
                        {
                            Id = te.EducationalStandart.Education.Id,
                            Name = te.EducationalStandart.Education.Name
                        }
                    })
                .ToList()
                })
                .FirstOrDefaultAsync();

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet("assigned")]
        public async Task<ActionResult<List<GetTaskDTO>>> GetAssignedTasks([FromQuery] int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.assignedTo.Id == userId)
                .Select(t => new GetTaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Start_Date = t.Start_Date,
                    DueDate = t.DueDate,
                    TaskStatus = t.TaskStatus,
                    assignedToDTO = new GetUserDTO
                    {
                        Id = t.assignedTo.Id,
                        Name = t.assignedTo.Name,
                        Email = t.assignedTo.Email,
                        Role = t.assignedTo.Role,
                    },
                    createdByDTO = new GetUserDTO
                    {
                        Id = t.createdBy.Id,
                        Name = t.createdBy.Name,
                        Email = t.createdBy.Email,
                        Role = t.createdBy.Role,
                    },
                    EducationalStandarts = t.Tasks_EducationalStandarts
                    .Select(te => new GetEducationalStandartsDTO
                    {
                        Id = te.EducationalStandart.Id,
                        Title = te.EducationalStandart.Title,
                        Description = te.EducationalStandart.Description,
                        Number = te.EducationalStandart.Number,
                        educationDTO = new GetEducationDTO
                        {
                            Id = te.EducationalStandart.Education.Id,
                            Name = te.EducationalStandart.Education.Name
                        }
                    })
                    .ToList()
                })
                .ToListAsync();

            return Ok(tasks);
        }


        [HttpGet]
        public async Task<ActionResult<List<GetTaskDTO>>> GetAllWaiting([FromQuery] int userId)
        {
            return Ok(await _context.Tasks
                .Where(t => t.createdBy.Id == userId
                            && t.TaskStatus.ToString() == "waitingForReview")
                .Select(t => new GetTaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Start_Date = t.Start_Date,
                    DueDate = t.DueDate,
                    TaskStatus = t.TaskStatus,
                    assignedToDTO = new GetUserDTO
                    {
                        Id = t.assignedTo.Id,
                        Name = t.assignedTo.Name,
                        Email = t.assignedTo.Email,
                        Role = t.assignedTo.Role,
                    },
                    createdByDTO = new GetUserDTO
                    {
                        Id = t.createdBy.Id,
                        Name = t.createdBy.Name,
                        Email = t.createdBy.Email,
                        Role = t.createdBy.Role,
                    },
                    EducationalStandarts = t.Tasks_EducationalStandarts
                    .Select(te => new GetEducationalStandartsDTO
                    {
                        Id = te.EducationalStandart.Id,
                        Title = te.EducationalStandart.Title,
                        Description = te.EducationalStandart.Description,
                        Number = te.EducationalStandart.Number,
                        educationDTO = new GetEducationDTO
                        {
                            Id = te.EducationalStandart.Education.Id,
                            Name = te.EducationalStandart.Education.Name
                        }
                    })
                    .ToList()
                })
                .ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<GetTaskDTO>> Create(CreateTaskDTO dto)
        {
            var assignedUser = await _context.Users.FindAsync(dto.assignedToId);
            var createdByUser = await _context.Users.FindAsync(dto.createdById);

            if (assignedUser == null || createdByUser == null)
                return BadRequest("One or more referenced entities do not exist.");

            var entity = new Tasks
            {
                Title = dto.Title,
                Description = dto.Description,
                Start_Date = dto.Start_Date,
                DueDate = dto.DueDate,
                TaskStatus = Status.Pending,
                assignedToId = dto.assignedToId,
                createdById = dto.createdById,
            };

            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync();
            foreach (int standard in dto.EducationalStandarts)
            {
                var educationStandard = await _context.EducationalStandarts.FindAsync(standard);
                if (educationStandard == null)
                    continue;
                var StandardEntrys = new Tasks_EducationalStandarts
                {
                    TaskId = entity.Id,
                    EducationalStandartId = standard,
                };
                _context.Tasks_EducationalStandarts.Add(StandardEntrys);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new GetTaskDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Start_Date = entity.Start_Date,
                DueDate = entity.DueDate,
                TaskStatus = Status.Pending,
                assignedToDTO = new GetUserDTO
                {
                    Id = entity.assignedTo.Id,
                    Name = entity.assignedTo.Name,
                    Email = entity.assignedTo.Email,
                    Role = entity.assignedTo.Role,
                },
                createdByDTO = new GetUserDTO
                {
                    Id = entity.createdBy.Id,
                    Name = entity.createdBy.Name,
                    Email = entity.createdBy.Email,
                    Role = entity.createdBy.Role,
                },

            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskDTO dto)
        {
            var entity = await _context.Tasks
                .Include(t => t.Tasks_EducationalStandarts)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
                return NotFound();

            var assignedUser = await _context.Users.FindAsync(dto.assignedToId);
            var createdByUser = await _context.Users.FindAsync(dto.createdById);

            if (assignedUser == null || createdByUser == null)
                return BadRequest("Assigned or CreatedBy user does not exist.");

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Start_Date = dto.Start_Date;
            entity.DueDate = dto.DueDate;
            entity.TaskStatus = dto.TaskStatus;
            entity.assignedTo = assignedUser;
            entity.createdBy = createdByUser;

            //Remove
            if (dto.RemoveEducationalStandarts.Any())
            {
                var toRemove = entity.Tasks_EducationalStandarts
                    .Where(te => dto.RemoveEducationalStandarts
                        .Contains(te.EducationalStandartId))
                    .ToList();

                _context.Tasks_EducationalStandarts.RemoveRange(toRemove);
            }

            //Add
            if (dto.AddEducationalStandartsId.Any())
            {
                var existingIds = entity.Tasks_EducationalStandarts
                    .Select(te => te.EducationalStandartId)
                    .ToList();

                var newIds = dto.AddEducationalStandartsId
                    .Where(id => !existingIds.Contains(id))
                    .ToList();

                foreach (var standardId in newIds)
                {
                    entity.Tasks_EducationalStandarts.Add(
                        new Tasks_EducationalStandarts
                        {
                            TaskId = entity.Id,
                            EducationalStandartId = standardId
                        });
                }
            }

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
