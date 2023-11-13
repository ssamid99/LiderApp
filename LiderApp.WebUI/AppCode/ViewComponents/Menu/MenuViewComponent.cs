using LiderApp.Domain.Business.MenuModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiderApp.WebUI.AppCode.ViewComponents.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public MenuViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = new MenuGetAllQuery();
            var response = await mediator.Send(query);
            return View(response);
        }
    }
}
