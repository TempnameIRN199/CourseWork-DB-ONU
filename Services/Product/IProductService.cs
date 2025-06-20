namespace CourseWork_DB_ONU.Services.Product
{
    using CourseWork_DB_ONU.Data.Models;
    public interface IProductService
    {
        List<Product> GetAll();
        string GetProductNameById(int id);
        int GetProductIdByName(string name);
        List<Product> SearchByName(string? namePart);
        void Add(Product product);
        void Delete(int id);
        void Edit(Product product);
    }
}