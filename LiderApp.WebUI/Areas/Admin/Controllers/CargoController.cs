using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Business.CargoModule;
using LiderApp.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CargoController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;
        private readonly ICargoInterface cargoInterface;

        public CargoController(LiderAppDbContext db, IMediator mediator, ICargoInterface cargoInterface)
        {
            this.db = db;
            this.mediator = mediator;
            this.cargoInterface = cargoInterface;
        }
        public async Task<ActionResult> Index(CargoGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View(response);
            }
        }


        // GET: CargoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CargoPostCommand command)
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

        
        public async Task<ActionResult> Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var entity = await db.Cargos.FirstOrDefaultAsync(c => c.Id == id && c.DeletedDate == null);

            var editCommand = cargoInterface.GetData();

            editCommand.Id = entity.Id;
            editCommand.Text = entity.Text;
            editCommand.ImagePath = entity.ImagePath;

            return View(editCommand);
        }

        // POST: CargoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CargoPutCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", command);
            }
            var response = await mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
