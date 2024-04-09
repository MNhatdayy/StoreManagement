namespace StoreManagement.Models
{
    public class Invoice : DeletableEntity
    {
        public int OrderId {  get; set; }
        public Order Order { get; set; }
        public double TotalPrice { get; set; } =0;
        public DateTime PayTime { get; set; } = DateTime.UtcNow;
        public string Note { get; set; } = string.Empty;
        public double Charge { get; set; } = 0;
        public bool Status { get; set; } = false;

    }
}
