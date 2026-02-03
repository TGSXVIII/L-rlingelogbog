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
                    Path = p.Path,
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
                    Path = p.Path,
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
                    Path = p.Path,
                    Type = p.Type
                })
                .ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<GetPicturesAndVideosDTO>> Create(CreatePicturesAndVideosDTO dto)
        {
            var item = new PicturesAndVideos
            {
                Path = dto.Path,
                Type = dto.Type,
                TaskId = dto.TaskId
            };

            _context.PicturesAndVideos.Add(item);
            await _context.SaveChangesAsync();

            return Ok(new GetPicturesAndVideosDTO
            {
                Id = item.Id,
                Path = item.Path,
                Type = item.Type
            });
        }
    }
}
