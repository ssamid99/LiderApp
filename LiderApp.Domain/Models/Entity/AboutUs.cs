using LiderApp.Domain.AppCode.Infrastucture;

namespace LiderApp.Domain.Models.Entity
{
    public class AboutUs : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public virtual ICollection<WebImage> WebImages { get; set; }
    }
}
