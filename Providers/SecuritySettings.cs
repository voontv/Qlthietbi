namespace QlThietBi.Providers
{
    public class SecuritySettings
    {
        public string Secret { get; set; } = string.Empty;

        public int Expiration { get; set; }

        public int PasswordLength { get; set; }
    }
}