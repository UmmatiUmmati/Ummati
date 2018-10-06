namespace Ummati
{
    using System;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Ummati.Constants;
    using Ummati.Controllers;

    public class Startup : IStartup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services) =>
            services
                .AddApplicationInsightsTelemetry(this.configuration)
                .AddCustomOptions(this.configuration)
                .AddResponseCaching()
                .AddCustomResponseCompression()
                .AddCustomStrictTransportSecurity()
                .AddRouting(options => options.LowercaseUrls = true)
                .AddCustomMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .Services
                .BuildServiceProvider();

        public void Configure(IApplicationBuilder application) =>
            application
                .UseForwardedHeaders(new ForwardedHeadersOptions() { ForwardedHeaders = ForwardedHeaders.XForwardedProto })
                .UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent())
                .UseResponseCaching()
                .UseResponseCompression()
                .UseIf(
                    !this.hostingEnvironment.IsDevelopment(),
                    x => x.UseHsts())
                .UseIf(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x.UseDeveloperExceptionPage())
                .UseDefaultFiles()
                .UseStaticFilesWithCacheControl()
                .UseXContentTypeOptions()
                .UseXDownloadOptions()
                .UseXfo(options => options.SameOrigin())
                .UseMvc(
                    routes =>
                    {
                        routes.MapRoute(
                            name: "default",
                            template: "{controller=Home}/{action=Index}/{id?}");

                        routes.MapSpaFallbackRoute(
                            name: "spa-fallback",
                            defaults: new { controller = ControllerName.Home, action = nameof(HomeController.Index) });
                    });
    }
}
