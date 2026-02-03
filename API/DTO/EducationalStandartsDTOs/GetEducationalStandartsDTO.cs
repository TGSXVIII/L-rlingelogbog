namespace API.DTO
{
    public class GetEducationalStandartsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public GetEducationDTO educationDTO { get; set; }
    }
}
