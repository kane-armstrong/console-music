using BeepBeep.Models;
using Pedantic.IO;
using System.Reflection;

namespace BeepBeep.Sheets
{
    public static class SheetFile
    {
        private static Assembly Assembly => typeof(SheetFile).Assembly;
        private static string Namespace => typeof(SheetFile).Namespace;

        public static ConsoleMusicSheet Load(string fileName)
        {
            return ConsoleMusicSheet.FromJson(EmbeddedResource.ReadAllText(Assembly, $"{Namespace}.{fileName}"));
        }
    }
}