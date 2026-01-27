namespace API.DTO
{
    public class GetTaskDTO
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
        public GetEducationalStandartsDTO educationStandartsId { get; set; }
        public GetUserDTO assignedToId { get; set; }
        public GetUserDTO createdById { get; set; }
    }
}
