namespace CourseWork_DB_ONU.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        [ForeignKey("Outlet")]
        public int OutletId { get; set; }
        public Outlet Outlet { get; set; }

        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
