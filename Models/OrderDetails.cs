namespace StoreManagement.Models
{
    public class OrderDetails : DeletableEntity
    {
        public int IdFood { get; set; }
        public decimal TotalPrice { get; set; }
        public int SumFood { get; set; }
        public string OderredDish { get; set; }
        public int IdOder {  get; set; }
        public Order Order { get; set; }

    }
}
