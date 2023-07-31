namespace SuperGroupAPI.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public string products { get; set; }
    }
}
