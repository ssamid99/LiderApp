using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Business.ServiceModule;
using LiderApp.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;
        private readonly IServiceInterface serviceInterface;

        public ServiceController(LiderAppDbContext db, IMediator mediator, IServiceInterface serviceInterface)
        {
            this.db = db;
            this.mediator = mediator;
            this.serviceInterface = serviceInterface;
        }
        public async Task<ActionResult> Index(ServiceGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: MenuController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServicePostCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", command);
            }
            else
            {
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: MenuController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NoContent();
            }

            var data = await db.Services.FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);


            var editCommand = serviceInterface.GetServiceData();
            editCommand.Id = data.Id;   
            editCommand.Name = data.Name;

            return View(editCommand);
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ServicePutCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", command);
            }
            else
            {
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ServiceRemoveCommand command)
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

        public async Task<ActionResult> DeletedIndex(ServiceRemovedGetAllQuery query)
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
        public async Task<ActionResult> GetBackDeletedServices(ServiceGetBackRemovedCommand command)
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
