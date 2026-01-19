namespace API.Models
{
    public class PicturesAndVideos
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public Tasks taskId { get; set; }
    }
}
