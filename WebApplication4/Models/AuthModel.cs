namespace WebApplication4.Models
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool IsAuth { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
    }
}
