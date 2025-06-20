namespace CourseWork_DB_ONU.Services.Order
{
    using CourseWork_DB_ONU.Data;
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.Services.Outlet;
    using CourseWork_DB_ONU.Services.Product;
    using CourseWork_DB_ONU.Services.Supplier;
    using CourseWork_DB_ONU.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;
        private readonly IOutletService _outletService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;

        public OrderService(DatabaseContext context,
            IOutletService outletService, ISupplierService supplierService,
            IProductService productService)
        {
            _context = context;
            _outletService = outletService;
            _supplierService = supplierService;
            _productService = productService;
        }
        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
        public List<OrderViewModel> ConvertToViewModel(List<Order> orders)
        {
            return orders.Select(s => new OrderViewModel
            {
                Id = s.Id,
                Date = s.Date,
                OutletName = _outletService.GetOutletNameById(s.OutletId),
                SupplierName = _supplierService.GetSupplierNameById(s.SupplierId),
                ProductName = _productService.GetProductNameById(s.ProductId),
                Amount = s.Amount,
                Price = s.Price
            }).ToList();
        }
        public void Add(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var order = _context.Orders
                .FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found");
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
        public void Edit(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }
            var existingOrder = _context.Orders
                .FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException($"Order with ID {order.Id} not found");
            }
            existingOrder.Date = order.Date;
            existingOrder.OutletId = order.OutletId;
            existingOrder.SupplierId = order.SupplierId;
            existingOrder.ProductId = order.ProductId;
            existingOrder.Amount = order.Amount;
            existingOrder.Price = order.Price;
            _context.SaveChanges();
        }
        public List<Order> SearchOrders(string? date, string? outlet,
            string? supplier, string? product, string? amount,
            string? price)
        {
            var query = _context.Orders
                .Include(o => o.Outlet)
                .Include(o => o.Supplier)
                .Include(o => o.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(date) && DateOnly.TryParse(date, out var parsedDate))
                query = query.Where(o => o.Date == parsedDate);

            if (!string.IsNullOrWhiteSpace(outlet))
                query = query.Where(o => o.Outlet.Name.Contains(outlet));

            if (!string.IsNullOrWhiteSpace(supplier))
                query = query.Where(o => o.Supplier.Name.Contains(supplier));

            if (!string.IsNullOrWhiteSpace(product))
                query = query.Where(o => o.Product.Name.Contains(product));

            if (!string.IsNullOrWhiteSpace(amount) && int.TryParse(amount, out var parsedAmount))
                query = query.Where(o => o.Amount == parsedAmount);

            if (!string.IsNullOrWhiteSpace(price) && double.TryParse(price, out var parsedPrice))
                query = query.Where(o => o.Price == parsedPrice);

            return query.ToList();
        }
    }
}
