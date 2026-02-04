namespace API.Models
{
    public class PicturesAndVideos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public enum Type
        {
            Image,
            Video
        }
        public int TaskId { get; set; }
        public Tasks Task { get; set; } = null!;
    }
}
