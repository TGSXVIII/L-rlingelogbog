namespace API.DTO
{
    public class GetEducationalStandartsDTO
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Description { get; set; }
        public GetEducationDTO educationDTO { get; set; }
    }
}
