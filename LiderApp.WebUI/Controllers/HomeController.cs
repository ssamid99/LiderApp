using LiderApp.Domain.Models.DbContexts;
using LiderApp.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LiderApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;

        public HomeController(LiderAppDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}