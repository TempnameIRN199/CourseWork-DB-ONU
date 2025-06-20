namespace CourseWork_DB_ONU.Services.EnumService
{
    using CourseWork_DB_ONU.Models;
    public class EnumService : IEnumService
    {
        public List<EnumOption<T>> GetEnumOptions<T>(string? filter = null) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>();

            var options = values.Select(v => new EnumOption<T>
            {
                Value = v,
                Description = v.GetDescription()
            });

            if (!string.IsNullOrWhiteSpace(filter))
            {
                options = options
                    .Where(o => o.Description.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            return options.ToList();
        }
    }
}
