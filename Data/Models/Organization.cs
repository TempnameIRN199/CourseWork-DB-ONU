namespace CourseWork_DB_ONU.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Organization
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string FullNameDirector { get; set; }

        [Required]
        public string TaxNumber { get; set; }
    }
}
