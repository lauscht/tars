namespace Kipp.Server.Options{
    public class JwtAuthorizationOptions{
        public string Authority {get;set;}
        public string Issuer {get;set;}
        public string Audience {get;set;}
    }
}