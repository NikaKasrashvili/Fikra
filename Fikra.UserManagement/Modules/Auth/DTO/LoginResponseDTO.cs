namespace Fikra.UserManagement.Modules.Auth.DTO
{
    public class LoginResponseDTO
    {
        public int? UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string JWT { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
        public string Message { get; set; }
    }
}
