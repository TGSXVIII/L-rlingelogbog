namespace API.Models
{
    public class Users_Education
    {
        public int Id { get; set; }
        public string Grade { get; set; }
        public Users UserId { get; set; }
        public Education educationId { get; set; }
    }
}
