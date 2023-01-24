using LanchesSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public AdminImagensController(IOptions<ConfigurationImagens> myConfig, IWebHostEnvironment hostingEnviroment)
        {
            _myConfig = myConfig.Value;
            _hostingEnviroment = hostingEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
