namespace Ummati.Controllers
{
    using System.IO;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Ummati.Constants;

    public class HomeController : Controller
    {
        private readonly string indexHtmlFilePath;

        public HomeController(IHostingEnvironment hostingEnvironment) =>
            this.indexHtmlFilePath = Path.Combine(hostingEnvironment.WebRootPath, "index.html");

        [ResponseCache(CacheProfileName = CacheProfileName.Index)]
        public IActionResult Index() =>
            this.File(System.IO.File.OpenRead(this.indexHtmlFilePath), ContentType.Html);
    }
}
