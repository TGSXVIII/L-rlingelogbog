namespace API.DTO
{
    public class CreatePicturesAndVideosDTO
    {
        public IFormFile File { get; set; }
        public int taskId { get; set; }
        public string Name { get; set; }
        public MediaType Type { get; set; }
    }
}
