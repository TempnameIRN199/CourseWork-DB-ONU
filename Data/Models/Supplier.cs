namespace CourseWork_DB_ONU.Data.Models
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using System.ComponentModel.DataAnnotations;
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public SupplierType Type { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
