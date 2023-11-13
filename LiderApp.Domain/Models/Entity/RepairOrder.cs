using LiderApp.Domain.AppCode.Infrastucture;

namespace LiderApp.Domain.Models.Entity
{
    public class RepairOrder : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        public string ServiceId { get; set; }
        public virtual Service Services { get; set; }
    }
}
