namespace API.DTO
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public enum Role
        {
            User,
            Admin
        }
    }
}
