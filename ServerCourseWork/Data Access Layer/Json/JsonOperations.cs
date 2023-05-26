using Newtonsoft.Json;
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

        public static T DeserealizeFromFile<T>(string path)
        {
            using FileStream fileStream = new(path, FileMode.OpenOrCreate);
            using StreamReader reader = new(fileStream);
            string jsonString = reader.ReadToEnd();
            return Deserealize<T>(jsonString);
        }
    }
}
