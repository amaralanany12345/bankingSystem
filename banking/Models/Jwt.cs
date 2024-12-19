namespace banking.Models
{
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int lifeTime { get; set; }
        public string SigningKey { get; set; }
    }
}
