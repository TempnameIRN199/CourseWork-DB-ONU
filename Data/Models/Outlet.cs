namespace CourseWork_DB_ONU.Data.Models
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Outlet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public OutletType Type { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string FullNameHeadOf { get; set; }
    }
}
