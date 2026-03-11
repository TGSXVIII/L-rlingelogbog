namespace API.Models
{
    public class EducationalStandarts
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Description { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
    }
}
