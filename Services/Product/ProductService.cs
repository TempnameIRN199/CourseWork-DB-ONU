namespace CourseWork_DB_ONU.Services.Product
{
    using CourseWork_DB_ONU.Data;
    using CourseWork_DB_ONU.Data.Models;
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;
        public ProductService(DatabaseContext context)
        {
            _context = context;
        }
        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public string GetProductNameById(int id)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id);
            return product?.Name ?? string.Empty;
        }
        public int GetProductIdByName(string name)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Name == name);
            return product?.Id ?? 0;
        }
        public List<Product> SearchByName(string? namePart)
        {
            if (string.IsNullOrWhiteSpace(namePart))
                return _context.Products.ToList();
            string lowered = namePart.ToLower();
            return _context.Products
                .Where(p => p.Name.ToLower().Contains(lowered))
                .ToList();
        }
        public void Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public void Edit(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }
            var existingProduct = _context.Products
                .FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found");
            }
            existingProduct.Name = product.Name;
            _context.SaveChanges();
        }
    }
}
