namespace API.DTO
{
    public class GetPicturesAndVideosDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public enum Type
        {
            Image,
            Video
        }
    }
}
