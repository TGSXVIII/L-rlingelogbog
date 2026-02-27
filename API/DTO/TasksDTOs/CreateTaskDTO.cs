namespace API.DTO
{
    public class CreateTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime DueDate { get; set; }
        public int assignedToId { get; set; }
        public int createdById { get; set; }
        public List<int> EducationalStandarts { get; set; } = new List<int>();
    }
}
