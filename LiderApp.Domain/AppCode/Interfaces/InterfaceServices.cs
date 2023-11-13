using LiderApp.Domain.Business.AboutUsModule;
using LiderApp.Domain.Business.CargoModule;
using LiderApp.Domain.Business.MenuModule;
using LiderApp.Domain.Business.ServiceModule;
using LiderApp.Domain.Business.WarrantyModule;
using Microsoft.Extensions.DependencyInjection;

namespace LiderApp.Domain.AppCode.Interfaces
{
    public class InterfaceServices
    {
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped<IMenuInterface, MenuPutCommand>();
            service.AddScoped<IServiceInterface, ServicePutCommand>();
            service.AddScoped<IWarrantyInterface, WarrantyPutCommand>();
            service.AddScoped<ICargoInterface, CargoPutCommand>();
            service.AddScoped<IAboutUsInterface, AboutUsPutCommand>();
        }
    }
}
