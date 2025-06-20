namespace CourseWork_DB_ONU.Services.Outlet
{
    using CourseWork_DB_ONU.Data;
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.Services.Organization;
    using CourseWork_DB_ONU.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class OutletService : IOutletService
    {
        private readonly DatabaseContext _context;
        private readonly IOrganizationService _organizationService;
        public OutletService(DatabaseContext context,
            IOrganizationService organizationService)
        {
            _context = context;
            _organizationService = organizationService;
        }
        public List<Outlet> GetAll()
        {
            return _context.Outlets.ToList();
        }
        public string GetOutletNameById(int id)
        {
            var outlet = _context.Outlets
                .FirstOrDefault(o => o.Id == id);
            return outlet?.Name ?? string.Empty;
        }
        public int GetOutletIdByName(string name)
        {
            var outlet = _context.Outlets
                .FirstOrDefault(o => o.Name == name);
            return outlet?.Id ?? 0;
        }
        public List<Outlet> SearchByName(string? namePart)
        {
            if (string.IsNullOrWhiteSpace(namePart))
                return _context.Outlets.ToList();
            string lowered = namePart.ToLower();
            return _context.Outlets
                .Where(o => o.Name.ToLower().Contains(lowered))
                .ToList();
        }
        public List<OutletViewModel> ConvertToViewModel
            (List<Outlet> _outlet)
        {
            return _outlet.Select(o => new OutletViewModel
            {
                Id = o.Id,
                Name = o.Name,
                Type = o.Type,
                OrganizationName = _organizationService.GetOrganizationNameById(o.OrganizationId),
                Address = o.Address,
                FullNameHeadOf = o.FullNameHeadOf
            }).ToList();
        }
        public void Add(Outlet outlet)
        {
            if (outlet == null)
            {
                throw new ArgumentNullException(nameof(outlet), "Outlet cannot be null");
            }
            _context.Outlets.Add(outlet);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var outlet = _context.Outlets
                .FirstOrDefault(o => o.Id == id);
            if (outlet == null)
            {
                throw new KeyNotFoundException($"Outlet with ID {id} not found");
            }
            _context.Outlets.Remove(outlet);
            _context.SaveChanges();
        }
        public void Edit(Outlet outlet)
        {
            if (outlet == null)
            {
                throw new ArgumentNullException(nameof(outlet), "Outlet cannot be null");
            }
            var existingOutlet = _context.Outlets
                .FirstOrDefault(o => o.Id == outlet.Id);
            if (existingOutlet == null)
            {
                throw new KeyNotFoundException($"Outlet with ID {outlet.Id} not found");
            }
            existingOutlet.Name = outlet.Name;
            existingOutlet.Type = outlet.Type;
            existingOutlet.OrganizationId = outlet.OrganizationId;
            existingOutlet.Address = outlet.Address;
            existingOutlet.FullNameHeadOf = outlet.FullNameHeadOf;
            _context.SaveChanges();
        }
        public List<Outlet> SearchOutlets(string? name, string? typeDescription, string? organizationName, string? address, string? headOf)
        {
            var query = _context.Outlets
                .Include(o => o.Organization)
                .AsEnumerable(); // ← важливо: переносимо виконання в пам’ять

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(o => o.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(typeDescription))
                query = query.Where(o => o.Type.GetDescription().Contains(typeDescription, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(organizationName))
                query = query.Where(o => o.Organization?.Name.Contains(organizationName, StringComparison.OrdinalIgnoreCase) == true);

            if (!string.IsNullOrWhiteSpace(address))
                query = query.Where(o => o.Address.Contains(address, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(headOf))
                query = query.Where(o => o.FullNameHeadOf.Contains(headOf, StringComparison.OrdinalIgnoreCase));

            return query.ToList();
        }
    }
}
