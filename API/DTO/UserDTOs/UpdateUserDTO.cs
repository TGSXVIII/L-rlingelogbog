namespace API.DTO
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public enum Role
        {
            User,
            Admin
        }
    }
}
