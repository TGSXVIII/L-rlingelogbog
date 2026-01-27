namespace API.DTO
{
    public class GetUsers_EducationDTO
    {
        public int Id { get; set; }
        public string Grade { get; set; }
        public GetUserDTO UserId { get; set; }
        public GetEducationDTO educationId { get; set; }
    }
}
