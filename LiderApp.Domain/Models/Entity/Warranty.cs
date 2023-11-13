using LiderApp.Domain.AppCode.Infrastucture;

namespace LiderApp.Domain.Models.Entity
{
    public class Warranty : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
    }
}
