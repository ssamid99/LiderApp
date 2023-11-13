using LiderApp.Domain.AppCode.Interfaces;
using LiderApp.Domain.Business.MenuModule;
using LiderApp.Domain.Business.SliderImagesModule;
using LiderApp.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LiderApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly LiderAppDbContext db;
        private readonly IMediator mediator;
        private readonly IMenuInterface menuInterface;

        public MenuController(LiderAppDbContext db, IMediator mediator, IMenuInterface menuInterface)
        {
            this.db = db;
            this.mediator = mediator;
            this.menuInterface = menuInterface;
        }
        public async Task<ActionResult> Index(MenuGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: MenuController/Details/5
        public async Task<ActionResult> Details(MenuGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: MenuController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ParentId = new SelectList(db.Menus.Where(m => m.DeletedDate == null).ToList(), "Id", "Name");
            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MenuPostCommand command)
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

            var data = await db.Menus.FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);


            var editCommand = menuInterface.GetMenuData();
            editCommand.Id = data.Id;   //5
            editCommand.Name = data.Name;
            editCommand.ControllerName = data.ControllerName;
            editCommand.ParentId = data.ParentId;  //eert
            ViewBag.ParentId = new SelectList(db.Menus.Where(m => m.Id != data.Id && m.DeletedDate == null).ToList(), "Id", "Name");

            return View(editCommand);
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MenuPutCommand command)
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
        public async Task<ActionResult> Delete(MenuRemoveCommand command)
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

        public async Task<ActionResult> DeletedIndex(MenuGetAllRemovedQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public async Task<ActionResult> DeletedDetails(MenuGetSingleRemovedQuery query)
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
        public async Task<ActionResult> GetBackDeletedMenus(MenuRemovedBackCommand command)
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

        public string GetName(string parentId)
        {
            var data = db.Menus.FirstOrDefault(m => m.Id == parentId && m.DeletedDate == null);
            if(data == null)
            {
                throw new ArgumentNullException(nameof(data), "Melumat Tapilmadi!");
            }
            return data.Name;
        }
    }
}
