namespace CourseWork_DB_ONU.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string OutletName { get; set; }
        public string SupplierName { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
