namespace CourseWork_DB_ONU.ViewModels
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using CourseWork_DB_ONU.Services;

    public class SellerViewModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string OutletName { get; set; }
        public string Position { get; set; }
        public DateOnly BirthDate { get; set; }
        public SexType Sex { get; set; }
        public string Address { get; set; }
        public string Residence { get; set; }
        public string TypeDescription => Sex.GetDescription();
    }
}
