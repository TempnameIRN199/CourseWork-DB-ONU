namespace CourseWork_DB_ONU.Services.Supplier
{
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.ViewModels;

    public interface ISupplierService
    {
        List<Supplier> GetAll();
        List<SupplierViewModel> ConvertToViewModel
            (List<Supplier> _supplier);
        string GetSupplierNameById(int id);
        int GetSupplierIdByName(string name);
        List<Supplier> SearchByName(string? namePart);
        void Add(Supplier supplier);
        void Delete(int id);
        void Edit(Supplier supplier);
        List<Supplier> SearchSuppliers(string? name,
            string? type, string? country, string? city,
            string? address);
    }
}