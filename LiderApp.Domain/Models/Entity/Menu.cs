using LiderApp.Domain.AppCode.Infrastucture;

namespace LiderApp.Domain.Models.Entity
{
    public class Menu : BaseEntity
    {
        public Menu(string name, string controllerName)
        {
            Name = name;
            ControllerName = controllerName;
        }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string? ActionId { get; set; }
        public string? ParentId { get; set; }
        public virtual Menu Parent { get; set; }
        public virtual ICollection<Menu> Children { get; set; }

    }
}
