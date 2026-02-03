namespace API.DTO
{
    public class GetUsers_EducationDTO
    {
        public int Id { get; set; }
        public string Grade { get; set; }
        public GetUserDTO UserDTO { get; set; }
        public GetEducationDTO educationDTO { get; set; }
    }
}
