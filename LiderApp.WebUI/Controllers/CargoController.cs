using LiderApp.Domain.Business.CargoModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiderApp.WebUI.Controllers
{
    public class CargoController : Controller
    {
        private readonly IMediator mediator;

        public CargoController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<ActionResult> Index(CargoGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NoContent();
            }
            return View(response);
        }
    }
}
