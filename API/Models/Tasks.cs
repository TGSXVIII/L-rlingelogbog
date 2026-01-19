namespace API.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime DueDate { get; set; }
        public enum Status 
        {
            Pending,
            InProgress,
            Completed
        }
        public EducationalStandarts educationStandartsId { get; set; }
        public Users userId { get; set; }
    }
}
