namespace API.DTO
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public enum Role
        {
            User,
            Admin
        }
    }
}
