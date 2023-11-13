using LiderApp.Domain.Business.SliderImagesModule;
using LiderApp.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LiderApp.WebUI.Areas.Admin.Controllers
{
    public class SliderImagesController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;

        public SliderImagesController(LiderAppDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }
        public async Task<ActionResult> Index(SliderImagesGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        // GET: SliderImagesController/Details/5
        public async Task<ActionResult> Details(SliderImagesGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: SliderImagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SliderImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SliderImagesPostCommand command)
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

        // GET: SliderImagesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NoContent();
            }

            var data = await db.SliderImages.FirstOrDefaultAsync(s => s.Id == id);

            var editCommand = new SliderImagesPutCommand();
            editCommand.Id = data.Id;
            editCommand.ImagePath = data.ImgPath;
            return View(editCommand);
        }

        // POST: SliderImagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SliderImagesPutCommand command)
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

        // POST: SliderImagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(SliderImagesRemoveCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", command);
            }
            else
            {
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
