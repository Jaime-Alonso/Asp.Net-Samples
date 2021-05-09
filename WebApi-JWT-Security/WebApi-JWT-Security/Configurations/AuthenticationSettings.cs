namespace WebApi_JWT_Security.Configurations
{
    public class AuthenticationSettings
    {
        public string Secret { get; set; }
        public int ExpirationDays { get; set; }
    }
}
