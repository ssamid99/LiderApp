using LiderApp.Domain.AppCode.Infrastucture;
using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Business.AboutUsModule;
using LiderApp.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutUsController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;
        private readonly IAboutUsInterface aboutUsInterface;

        public AboutUsController(LiderAppDbContext db, IMediator mediator, IAboutUsInterface aboutUsInterface)
        {
            this.db = db;
            this.mediator = mediator;
            this.aboutUsInterface = aboutUsInterface;
        }
        public async Task<ActionResult> Index(AboutUsGetAllQuery query)
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
        public async Task<ActionResult> Create(AboutUsPostCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await db.AboutUs
                .Include(a=>a.WebImages)
                .FirstOrDefaultAsync(a => a.Id == id && a.DeletedDate == null);

            var editCommand = aboutUsInterface.GetData();

            editCommand.Id = entity.Id;
            editCommand.Title = entity.Title;
            editCommand.Text = entity.Text;
            editCommand.Images = entity.WebImages.Select(x => new ImageItem
            {
                Id = x.Id,
                TempPath = x.Name,
                IsMain = x.IsMain
            }).ToArray();

            return View(editCommand);
        }

        // POST: CargoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AboutUsPutCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NoContent();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
