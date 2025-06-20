namespace CourseWork_DB_ONU.Data.Models
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Seller
    {
        public int Id { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        [ForeignKey("Outlet")]
        public int OutletId { get; set; }
        public Outlet Outlet { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public DateOnly BirthDate { get; set; }
        [Required]
        public SexType Sex { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Residence { get; set; }
    }
}
