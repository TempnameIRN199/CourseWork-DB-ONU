namespace CourseWork_DB_ONU.Services.Organization
{
    using CourseWork_DB_ONU.Data.Models;
    public interface IOrganizationService
    {
        List<Organization> GetAll();
        string GetOrganizationNameById(int id);
        int GetOrganizationIdByName(string name);
        List<Organization> SearchByName(string? namePart);
        void Add(Organization organization);
        void Delete(int id);
        void Edit(Organization organization);
        List<Organization> SearchOrganizations(string? name,
            string? address, string? director, string? taxNumber);
    }
}
