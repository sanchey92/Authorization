namespace Authorization.JwtBearer
{
    public static class Constants
    {
        public const string Issuer = "https://localhost:5001";
        public const string Audience = Issuer;
        public const string SecretKey = "this_is_long_secret_string_for_jwt_token";
    }
}