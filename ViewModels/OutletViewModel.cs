namespace CourseWork_DB_ONU.ViewModels
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using CourseWork_DB_ONU.Services;

    public class OutletViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OutletType Type { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string FullNameHeadOf { get; set; }
        public string TypeDescription => Type.GetDescription();
    }
}
