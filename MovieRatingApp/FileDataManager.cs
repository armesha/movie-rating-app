    using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MovieRatingApp.Utilities
{
    public static class FileDataManager
    {
        private static readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;

        public static void SaveData<T>(List<T> data, string fileName)
        {
            string fullPath = Path.Combine(basePath, fileName + ".json");
            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fullPath, jsonString);
        }

        public static List<T> LoadData<T>(string fileName)
        {
            string fullPath = Path.Combine(basePath, $"{fileName}.json");
            try
            {
                if (!File.Exists(fullPath))
                {
                    MessageBox.Show($"File not found: {fullPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<T>();
                }

                string jsonString = File.ReadAllText(fullPath);
                return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading {fileName}.json: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<T>();
            }
        }
    }
}
