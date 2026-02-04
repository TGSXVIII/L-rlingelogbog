namespace API.DTO
{
    public class UpdatePicturesAndVideosDTO
    {
        public string Path { get; set; }
        public enum Type
        {
            Image,
            Video
        }
        public int taskId { get; set; }
    }
}
