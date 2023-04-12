namespace Syntax.WebApp.Internal.Models
{
    public class TokenClass
    {
        public string Token { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
    }
}
