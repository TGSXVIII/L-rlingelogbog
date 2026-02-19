namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class PicturesAndVideosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PicturesAndVideosController(AppDbContext context)
        {
            _context = context;
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

        //[HttpPost]
        //[Consumes("multipart/form-data")]
        //public async Task<ActionResult<GetPicturesAndVideosDTO>> Create(
        //    [FromForm] CreatePicturesAndVideosDTO dto)
        //{
        //    if (dto.File == null || dto.File.Length == 0)
        //        return BadRequest("File is required");

        //    // Validate file type
        //    var allowedTypes = new[] { "image/", "video/" };
        //    if (!allowedTypes.Any(t => dto.File.ContentType.StartsWith(t)))
        //        return BadRequest("Only images and videos are allowed");

        //    // Create uploads directory
        //    if (dto.Type.ToString() == "Image")
        //    {
        //        var uploadsRoot = Name.Combine(Directory.GetCurrentDirectory(), "images");
        //        Directory.CreateDirectory(uploadsRoot);
        //    }
        //    else if (dto.Type.ToString() == "Video")
        //    {
        //        var uploadsRoot = Name.Combine(Directory.GetCurrentDirectory(), "videos");
        //        Directory.CreateDirectory(uploadsRoot);
        //    }

        //    // Generate safe filename
        //    var extension = Name.GetExtension(dto.File.FileName);
        //    var fileName = $"{Guid.NewGuid()}{extension}";
        //    var fileName = Name.Combine(uploadsRoot, fileName);

        //    // Save file to disk
        //    await using (var stream = new FileStream(fileName, FileMode.Create))
        //    {
        //        await dto.File.CopyToAsync(stream);
        //    }

        //    // Save DB record
        //    var item = new PicturesAndVideos
        //    {
        //        Name = fileName,
        //        Type = dto.Type,
        //        TaskId = dto.TaskId
        //    };

        //    _context.PicturesAndVideos.Add(item);
        //    await _context.SaveChangesAsync();

        //    return Ok(new GetPicturesAndVideosDTO
        //    {
        //        Id = item.Id,
        //        Name = item.Name,
        //        Type = item.Type
        //    });
        //}

    }
}
