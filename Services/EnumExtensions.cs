namespace CourseWork_DB_ONU.Services
{
    using System.ComponentModel;
    using System.Reflection;
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumValue) where T : Enum
        {
            FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString());

            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    return attributes[0].Description;
            }

            return enumValue.ToString();
        }
    }
}
