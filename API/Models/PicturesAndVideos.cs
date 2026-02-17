namespace API.Models
{
    public enum MediaType
    {
        Image,
        Video
    }
    public class PicturesAndVideos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MediaType Type { get; set; }
        public int TaskId { get; set; }
        public Tasks Task { get; set; } = null!;
    }
}
