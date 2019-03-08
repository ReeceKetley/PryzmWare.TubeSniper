namespace TubeSniper.Core.Application.Proxies
{
    public class HttpProxyDto
    {
        public string Address { get; set; }
        public string CredentialsUsername { get; set; }
        public string CredentialsPassword { get; set; }

        public HttpProxyDto ShallowCopy()
        {
            return (HttpProxyDto)MemberwiseClone();
        }
    }
}
