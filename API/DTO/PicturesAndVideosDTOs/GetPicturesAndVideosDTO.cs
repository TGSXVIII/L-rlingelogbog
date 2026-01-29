namespace API.DTO
{
    public class GetPicturesAndVideosDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public GetTaskDTO taskId { get; set; }
    }
}
