using System.Configuration;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class PicturesAndVideosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public PicturesAndVideosController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetPicturesAndVideosDTO>>> GetAll()
        {
            return Ok(await _context.PicturesAndVideos
                .Select(p => new GetPicturesAndVideosDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Type,
                })
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPicturesAndVideosDTO>> GetById(int id)
        {
            var item = await _context.PicturesAndVideos
                .Where(p => p.Id == id)
                .Select(p => new GetPicturesAndVideosDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Type
                })
                .FirstOrDefaultAsync();
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<List<GetPicturesAndVideosDTO>>> GetByTaskId(int taskId)
        {
            return Ok(await _context.PicturesAndVideos
                .Where(p => p.TaskId == taskId)
                .Select(p => new GetPicturesAndVideosDTO
                {
                    //need to change when we know where the files are stored
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Type
                })
                .ToListAsync());
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<GetPicturesAndVideosDTO>> Create(
        [FromForm] CreatePicturesAndVideosDTO dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File is required");

            // Validate file type
            var allowedTypes = new[] { "image/", "video/" };
            if (!allowedTypes.Any(t => dto.File.ContentType.StartsWith(t)))
                return BadRequest("Only images and videos are allowed");

            // Create uploads directory
            string subFolder;

            if (dto.File.ContentType.StartsWith("image/"))
            {
                subFolder = "images";
            }
            else if (dto.File.ContentType.StartsWith("video/"))
            {
                subFolder = "videos";
            }
            else
            {
                return BadRequest("Unsupported file type");
            }

            // Build full folder path
            var uploadsRoot = _configuration["FileStorage:UploadPath"];

            if (string.IsNullOrEmpty(uploadsRoot))
                return StatusCode(500, "Upload path not configured.");

            var finalFolderPath = Path.Combine(uploadsRoot, subFolder);

            // Create directory
            Directory.CreateDirectory(finalFolderPath);

            // Generate safe filename
            var extension = Path.GetExtension(dto.File.FileName);
            var safeFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(finalFolderPath, safeFileName);

            // Save uploads file to remote server
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // Save DB record
            var relativePath = $"{subFolder}/{safeFileName}";

            var item = new PicturesAndVideos
            {
                Name = safeFileName,
                Type = dto.Type,
                TaskId = dto.taskId
            };

            _context.PicturesAndVideos.Add(item);
            await _context.SaveChangesAsync();

            var publicUrl = $"/uploads/{subFolder}/{safeFileName}";

            return Ok(new GetPicturesAndVideosDTO
            {
                Id = item.Id,
                Name = publicUrl,
                Type = item.Type
            });
        }

    }
}
