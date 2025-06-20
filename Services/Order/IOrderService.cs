namespace CourseWork_DB_ONU.Services.Order
{
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.ViewModels;

    public interface IOrderService
    {
        List<Order> GetAll();
        List<OrderViewModel> ConvertToViewModel(List<Order> orders);
        void Add(Order order);
        void Delete(int id);
        void Edit(Order order);
        List<Order> SearchOrders(string? date, string? outlet, string? supplier,
            string? product, string? amount, string? price);

    }
}
