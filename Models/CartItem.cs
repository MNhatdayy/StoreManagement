namespace StoreManagement.Models
{
    public class CartItem
    {
        public int IdFood { get; set; }
        public string NameFood { get; set; }
        public string Picture {  get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
        public double TotalPrice => Quantity * Price;
    }
}
