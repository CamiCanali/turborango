using System.ComponentModel;

namespace TurboRango.Dominio.Utils
{
    public static class EnumExtensions
    {
        // Thanks to: http://stackoverflow.com/a/1799401
        public static string GetDescription<T>(this T value)
        {
            var type = typeof(T);
            var memInfo = type.GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
