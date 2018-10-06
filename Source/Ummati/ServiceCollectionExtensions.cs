namespace Ummati
{
    using System;
    using System.IO.Compression;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Ummati.Options;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddSingleton(configuration)
                .Configure<ApplicationOptions>(configuration)
                .AddSingleton(x => x.GetRequiredService<IOptions<ApplicationOptions>>().Value)
                .Configure<CacheProfileOptions>(configuration.GetSection(nameof(ApplicationOptions.CacheProfiles)))
                .AddSingleton(x => x.GetRequiredService<IOptions<CacheProfileOptions>>().Value)
                .Configure<CompressionOptions>(configuration.GetSection(nameof(ApplicationOptions.Compression)))
                .AddSingleton(x => x.GetRequiredService<IOptions<CompressionOptions>>().Value)
                .Configure<ForwardedHeadersOptions>(configuration.GetSection(nameof(ApplicationOptions.ForwardedHeaders)))
                .Configure<RedisOptions>(configuration.GetSection(nameof(ApplicationOptions.Redis)))
                .AddSingleton(x => x.GetRequiredService<IOptions<RedisOptions>>().Value);

        public static IServiceCollection AddCustomResponseCompression(this IServiceCollection services) =>
            services
                .AddResponseCompression(
                    options =>
                    {
                        // Add additional MIME types (other than the built in defaults) to enable GZIP compression for.
                        var customMimeTypes = services
                            .BuildServiceProvider()
                            .GetRequiredService<CompressionOptions>()
                            .MimeTypes ?? Enumerable.Empty<string>();
                        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(customMimeTypes);
                    })
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

        public static IServiceCollection AddCustomStrictTransportSecurity(this IServiceCollection services) =>
            services
                .AddHsts(
                    options =>
                    {
                        // Preload the HSTS HTTP header for better security. See https://hstspreload.org/
                        options.IncludeSubDomains = true;
                        options.MaxAge = TimeSpan.FromSeconds(31536000); // 1 Year
                        options.Preload = true;
                    });

        public static IMvcBuilder AddCustomMvc(this IServiceCollection services) =>
            services.AddMvc(
                options =>
                {
                    // Controls how controller actions cache content from the appsettings.json file.
                    var cacheProfileOptions = services
                        .BuildServiceProvider()
                        .GetRequiredService<CacheProfileOptions>();
                    foreach (var keyValuePair in cacheProfileOptions)
                    {
                        options.CacheProfiles.Add(keyValuePair);
                    }
                });
    }
}
