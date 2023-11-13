using LiderApp.Domain.Business.RepairOrderModule;
using LiderApp.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiderApp.WebUI.Controllers
{
    public class RepairOrderController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;

        public RepairOrderController(LiderAppDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }
        public ActionResult Index()
        {
            ViewBag.Services = new SelectList(db.Services.Where(m => m.DeletedDate == null).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RepairOrderPostCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", command);
            }
            else
            {
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index), controllerName:"Home");
            }

        }
    }
}
