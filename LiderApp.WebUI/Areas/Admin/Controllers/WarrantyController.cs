using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Business.WarrantyModule;
using LiderApp.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WarrantyController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;
        private readonly IWarrantyInterface warrantyInterface;

        public WarrantyController(LiderAppDbContext db, IMediator mediator, IWarrantyInterface warrantyInterface)
        {
            this.db = db;
            this.mediator = mediator;
            this.warrantyInterface = warrantyInterface;
        }

        // GET: WarrantyController/Details/5
        public async Task<ActionResult> Index(WarrantyGetAllQuery query)
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

        // GET: WarrantyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WarrantyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WarrantyPostCommand command)
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
        [HttpGet]
        // GET: WarrantyController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await db.Warranties.FirstOrDefaultAsync(w => w.Id == id && w.DeletedDate == null);

            var editCommand = warrantyInterface.GetData();
            editCommand.Id = data.Id;
            editCommand.Title = data.Title;
            editCommand.Text = data.Text;
            editCommand.ImagePath = data.ImagePath;

            return View(editCommand);
        }

        // POST: WarrantyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WarrantyPutCommand command)
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
