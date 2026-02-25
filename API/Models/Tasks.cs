namespace API.Models
{
    public enum Status
    {
        Pending,
        InProgress,
        waitingForReview,
        Completed
    }
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime DueDate { get; set; }
        public Status TaskStatus { get; set; }
        public int assignedToId { get; set; }
        public Users assignedTo { get; set; }
        public int createdById { get; set; }
        public Users createdBy { get; set; }
        public ICollection<PicturesAndVideos> PicturesAndVideos { get; set; }
        = new List<PicturesAndVideos>();
    }
}
