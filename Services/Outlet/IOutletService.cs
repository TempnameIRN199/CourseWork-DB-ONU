namespace CourseWork_DB_ONU.Services.Outlet
{
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.ViewModels;

    public interface IOutletService
    {
        List<Outlet> GetAll();
        string GetOutletNameById(int id);
        int GetOutletIdByName(string name);
        List<Outlet> SearchByName(string? namePart);
        List<OutletViewModel> ConvertToViewModel
            (List<Outlet> _outlet);
        void Add(Outlet outlet);
        void Delete(int id);
        void Edit(Outlet outlet);
        List<Outlet> SearchOutlets(string? name, string? type,
            string? organizationName, string? address, string? headOf);
    }
}
