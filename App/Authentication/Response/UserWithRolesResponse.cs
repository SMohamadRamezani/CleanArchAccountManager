namespace App.Authentication.Response
{
    public class UserWithRolesResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
