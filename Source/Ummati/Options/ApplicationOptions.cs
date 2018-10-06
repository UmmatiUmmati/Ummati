namespace Ummati.Options
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    public class ApplicationOptions
    {
        public CacheProfileOptions CacheProfiles { get; set; }

        public CompressionOptions Compression { get; set; }

        public ForwardedHeadersOptions ForwardedHeaders { get; set; }

        public KestrelServerOptions Kestrel { get; set; }

        public RedisOptions Redis { get; set; }
    }
}
