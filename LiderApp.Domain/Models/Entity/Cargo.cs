using LiderApp.Domain.AppCode.Infrastucture;

namespace LiderApp.Domain.Models.Entity
{
    public class Cargo : BaseEntity
    {
        public string Text { get; set; }
        public string ImagePath { get; set; }
    }
}
