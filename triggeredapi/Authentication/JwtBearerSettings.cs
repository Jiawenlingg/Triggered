namespace triggeredapi.Authentication
{
    public class JwtBearerSettings
    {
        public string Issuer {get; set; }
        public string Audience {get; set;}
        public string SigningKey {get; set; }
    }
}