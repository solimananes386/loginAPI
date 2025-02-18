namespace WebApplication4.Models
{
    public class Jwt
    {
      public string Secret { get; set; }
      public string Issuer { get; set; }
      public string Audience { get; set; }
      public double DurationInDay { get; set; }
    }
}
