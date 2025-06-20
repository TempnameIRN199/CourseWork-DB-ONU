namespace CourseWork_DB_ONU.ViewModels
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using CourseWork_DB_ONU.Services;
    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SupplierType Type { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string TypeDescription => Type.GetDescription();
    }

}
