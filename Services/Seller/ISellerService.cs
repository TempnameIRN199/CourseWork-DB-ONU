namespace CourseWork_DB_ONU.Services.Seller
{
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.ViewModels;

    public interface ISellerService
    {
        List<Seller> GetAll();
        List<SellerViewModel> ConvertToViewModel(List<Seller> sellers);
        void Add(Seller seller);
        void Delete(int id);
        void Edit(Seller seller);
        List<Seller> SearchSellers(string? surname, string? name,
            string? patronymic, string? outletName, string? position,
            string? sexDescription, string? address, string? residence);
    }
}