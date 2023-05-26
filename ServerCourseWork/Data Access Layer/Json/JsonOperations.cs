using Newtonsoft.Json;
using System;
using System.IO;

namespace ServerCourseWork.Data_Access_Layer.Json
{
    static class JsonOperations
    {
        public static string Serilalize<T>(T item) => JsonConvert.SerializeObject(item);

        public static T Deserealize<T>(string jsonString) => JsonConvert.DeserializeObject<T>(jsonString);
        public static void SerilalizeToFile<T>(string path, T item)
        {
            string jsonString = Serilalize(item);
            using StreamWriter writer = new(path);
            writer.Write(jsonString);
        }

        public static T DeserealizeFromFile<T>(string path) where T : new()
        {
            using FileStream fileStream = new(path, FileMode.OpenOrCreate);
            using StreamReader reader = new(fileStream);
            string jsonString = reader.ReadToEnd();

            try
            {
                return Deserealize<T>(jsonString);
            }
            catch (JsonException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Source: {ex.Source}, ErrorMessage: {ex.Message}");
                Console.WriteLine($"StackTrace:\n {ex.StackTrace}");
                Console.WriteLine("A new instance of the object has been created with default fields!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                return new();
            }
        }
    }
}
