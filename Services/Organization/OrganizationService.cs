namespace CourseWork_DB_ONU.Services.Organization
{
    using CourseWork_DB_ONU.Data;
    using CourseWork_DB_ONU.Data.Models;
    using System;
    using System.Linq;
    public class OrganizationService : IOrganizationService
    {
        private readonly DatabaseContext _context;
        public OrganizationService(DatabaseContext context)
        {
            _context = context;
        }
        public List<Organization> GetAll()
        {
            return _context.Organizations.ToList();
        }
        public string GetOrganizationNameById(int _id)
        {
            var organization = _context.Organizations
                .FirstOrDefault(o => o.Id == _id);
            return organization?.Name ?? string.Empty;
        }
        public int GetOrganizationIdByName(string name)
        {
            var organization = _context.Organizations
                .FirstOrDefault(o => o.Name == name);
            return organization?.Id ?? 0;
        }
        public List<Organization> SearchByName(string? namePart)
        {
            if (string.IsNullOrWhiteSpace(namePart))
                return _context.Organizations.ToList();

            string lowered = namePart.ToLower();

            return _context.Organizations
                .Where(o => o.Name.ToLower().Contains(lowered))
                .ToList();
        }
        public void Add(Organization organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException(nameof(organization), "Organization cannot be null");
            }
            _context.Organizations.Add(organization);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var organization = _context.Organizations
                .FirstOrDefault(o => o.Id == id);
            if (organization == null)
            {
                throw new KeyNotFoundException($"Organization with ID {id} not found");
            }
            _context.Organizations.Remove(organization);
            _context.SaveChanges();
        }

        public void Edit(Organization organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException(nameof(organization), "Organization cannot be null");
            }
            var existingOrganization = _context.Organizations
                .FirstOrDefault(o => o.Id == organization.Id);
            if (existingOrganization == null)
            {
                throw new KeyNotFoundException($"Organization with ID {organization.Id} not found");
            }
            existingOrganization.Name = organization.Name;
            existingOrganization.Address = organization.Address;
            existingOrganization.FullNameDirector = organization.FullNameDirector;
            existingOrganization.TaxNumber = organization.TaxNumber;
            _context.SaveChanges();
        }
        public List<Organization> SearchOrganizations(string? name, string? address, string? director, string? taxNumber)
        {
            var query = _context.Organizations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(o => o.Name.ToLower().Contains(name.ToLower()));

            if (!string.IsNullOrWhiteSpace(address))
                query = query.Where(o => o.Address.ToLower().Contains(address.ToLower()));

            if (!string.IsNullOrWhiteSpace(director))
                query = query.Where(o => o.FullNameDirector.ToLower().Contains(director.ToLower()));

            if (!string.IsNullOrWhiteSpace(taxNumber))
                query = query.Where(o => o.TaxNumber.ToLower().Contains(taxNumber.ToLower()));

            return query.ToList();
        }
    }
}
