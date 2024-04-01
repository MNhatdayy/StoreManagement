namespace StoreManagement.DTO
{
    public class InvoiceDTO 
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime PayTime { get; set; } 
        public string Note { get; set; } 
        public double Charge { get; set; } 
        public double TotalPrice { get; set; } 


    }
}
