namespace API.Models
{
    public class EducationalStandarts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
    }
}
