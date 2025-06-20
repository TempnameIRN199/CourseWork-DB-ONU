namespace CourseWork_DB_ONU.Services.EnumService
{
    using CourseWork_DB_ONU.Models;
    public interface IEnumService
    {
        List<EnumOption<T>> GetEnumOptions<T>(string? filter = null) where T : Enum;
    }
}
