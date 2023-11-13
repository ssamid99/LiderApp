using LiderApp.Domain.Business.RepairOrderModule;
using LiderApp.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiderApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RepairOrderController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;

        public RepairOrderController(LiderAppDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }
        public async Task<ActionResult> Index(RepairOrderGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public async Task<ActionResult> Details(RepairOrderGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(RepairOrderRemoveCommand command)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> DeletedIndex(RepairOrderRemovedGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public async Task<ActionResult> DeletedDetails(RepairOrderRemovedGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetBackDeletedServices(RepairOrderGetRemovedBackCommand command)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
