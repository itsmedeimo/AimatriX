using System;
using System.IO;
using System.Text.Json;

namespace AimatriX
{
    public class Settings
    {
        private static readonly string ConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AimatriX", "settings.json");

        public string SelectedCrosshair { get; set; } = "Resources/crosshair.png";

        public static Settings Load()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    string json = File.ReadAllText(ConfigPath);
                    return JsonSerializer.Deserialize<Settings>(json);
                }
            }
            catch
            {
                // You could log the error here if needed
            }

            return new Settings(); // fallback to default
        }

        public void Save()
        {
            try
            {
                var dir = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);
            }
            catch
            {
                // You could log the error here if needed
            }
        }
    }
}
