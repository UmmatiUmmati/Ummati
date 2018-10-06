namespace Ummati
{
    using System;
    using System.Linq;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Ummati.Constants;
    using Ummati.Options;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
        {
            var cacheProfileOptions = application
                .ApplicationServices
                .GetRequiredService<CacheProfileOptions>();
            var serviceWorkerCacheProfile = cacheProfileOptions
                .Where(x => string.Equals(x.Key, CacheProfileName.ServiceWorker, StringComparison.Ordinal))
                .Select(x => x.Value)
                .SingleOrDefault() ??
                throw new InvalidOperationException("CacheProfiles.ServiceWorker section is missing in appsettings.json");
            var staticFilesCacheProfile = cacheProfileOptions
                .Where(x => string.Equals(x.Key, CacheProfileName.StaticFiles, StringComparison.Ordinal))
                .Select(x => x.Value)
                .SingleOrDefault() ??
                throw new InvalidOperationException("CacheProfiles.StaticFiles section is missing in appsettings.json");
            return application
                .UseStaticFiles(
                    new StaticFileOptions()
                    {
                        OnPrepareResponse = context =>
                        {
                            if (string.Equals(context.File.Name, "service-worker.js", StringComparison.OrdinalIgnoreCase))
                            {
                                context.Context.ApplyCacheProfile(serviceWorkerCacheProfile);
                            }
                            else
                            {
                                context.Context.ApplyCacheProfile(staticFilesCacheProfile);
                            }
                        }
                    });
        }
    }
}
