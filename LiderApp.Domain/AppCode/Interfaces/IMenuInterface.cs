using LiderApp.Domain.Business.MenuModule;
using LiderApp.Domain.Business.SliderImagesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.AppCode.Interfaces
{
    public interface IMenuInterface
    {
        MenuPutCommand GetMenuData();
    }
}
