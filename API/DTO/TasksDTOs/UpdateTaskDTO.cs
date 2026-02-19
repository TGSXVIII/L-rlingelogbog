namespace API.DTO
{
    public class UpdateTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime DueDate { get; set; }
        public Status TaskStatus { get; set; }
        public int educationStandartsId { get; set; }
        public int assignedToId { get; set; }
        public int createdById { get; set; }
    }
}
