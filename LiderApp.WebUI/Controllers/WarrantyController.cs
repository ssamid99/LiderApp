using LiderApp.Domain.Business.WarrantyModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiderApp.WebUI.Controllers
{
    public class WarrantyController : Controller
    {
        private readonly IMediator mediator;

        public WarrantyController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        // GET: WarrantyController
        public async Task<ActionResult> Index(WarrantyGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if(response == null)
            {
                return NoContent();
            }
            return View(response);
        }
    }
}
