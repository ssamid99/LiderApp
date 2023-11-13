namespace LiderApp.Domain.Models.Entity
{
    public class WebImage 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public string? AboutUsId { get; set; }
        public virtual AboutUs AboutUs { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.AddHours(4);
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? CreatedbyUserId { get; set; }
    }
}
