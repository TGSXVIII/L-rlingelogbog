namespace API.Models
{
    public class Users_Education
    {
        public int Id { get; set; }
        public string Grade { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
    }
}
