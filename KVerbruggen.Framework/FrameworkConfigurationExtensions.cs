using System.Globalization;

namespace KVerbruggen.Framework
{
    public static class FrameworkConfigurationExtensions
    {
        /// <summary>
        /// Set the app language
        /// </summary>
        public static void SetAsAppLanguage(this string ietfLanguageTag)
        {
            CultureInfo appCulture = new(ietfLanguageTag);
            Thread.CurrentThread.CurrentCulture = appCulture;
            Thread.CurrentThread.CurrentUICulture = appCulture;
        }
    }
}
