namespace CoffeeApplication.Data.Entities
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public decimal? Percentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
