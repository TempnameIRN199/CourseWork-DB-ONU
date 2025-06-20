namespace CourseWork_DB_ONU.Services.Supplier
{
    using CourseWork_DB_ONU.Data;
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.ViewModels;

    public class SupplierService : ISupplierService
    {
        private readonly DatabaseContext _context;

        public SupplierService(DatabaseContext context)
        {
            _context = context;
        }

        public List<Supplier> GetAll()
        {
            return _context.Suppliers.ToList();
        }
        public List<SupplierViewModel> ConvertToViewModel
            (List<Supplier> supplier)
        {
            return supplier.Select(s => new SupplierViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Type = s.Type,
                Country = s.Country,
                City = s.City,
                Address = s.Address
            }).ToList();
        }
        public string GetSupplierNameById(int id)
        {
            var supplier = _context.Suppliers
                .FirstOrDefault(s => s.Id == id);
            return supplier?.Name ?? string.Empty;
        }
        public int GetSupplierIdByName(string name)
        {
            var supplier = _context.Suppliers
                .FirstOrDefault(s => s.Name == name);
            return supplier?.Id ?? 0;
        }
        public List<Supplier> SearchByName(string? namePart)
        {
            if (string.IsNullOrWhiteSpace(namePart))
                return _context.Suppliers.ToList();
            string lowered = namePart.ToLower();
            return _context.Suppliers
                .Where(s => s.Name.ToLower().Contains(lowered))
                .ToList();
        }
        public void Add(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null");
            }
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var supplier = _context.Suppliers
                .FirstOrDefault(s => s.Id == id);
            if (supplier == null)
            {
                throw new KeyNotFoundException($"Supplier with ID {id} not found");
            }
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
        public void Edit(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier), "Supplier cannot be null");
            }
            var existingSupplier = _context.Suppliers
                .FirstOrDefault(s => s.Id == supplier.Id);
            if (existingSupplier == null)
            {
                throw new KeyNotFoundException($"Supplier with ID {supplier.Id} not found");
            }
            existingSupplier.Name = supplier.Name;
            existingSupplier.Type = supplier.Type;
            existingSupplier.Country = supplier.Country;
            existingSupplier.City = supplier.City;
            existingSupplier.Address = supplier.Address;
            _context.SaveChanges();
        }
        public List<Supplier> SearchSuppliers(string? name, string? type, string? country, string? city, string? address)
        {
            var query = _context.Suppliers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(s => s.Name.ToLower()
                .Contains(name.ToLower()));
            if (!string.IsNullOrWhiteSpace(country))
                query = query.Where(s => s.Country.ToLower()
                .Contains(country.ToLower()));
            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(s => s.City.ToLower()
                .Contains(city.ToLower()));
            if (!string.IsNullOrWhiteSpace(address))
                query = query.Where(s => s.Address.ToLower()
                .Contains(address.ToLower()));

            var list = query.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(type))
            {
                list = list.Where(s => s.Type.GetDescription()
                .ToLower().Contains(type.ToLower()));
            }
            return list.ToList();
        }

    }
}
