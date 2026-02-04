namespace API.DTO
{
    public class CreatePicturesAndVideosDTO
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public enum Type
        {
            Image,
            Video
        }
        public int taskId { get; set; }
    }
}
