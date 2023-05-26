using ServerCourseWork.Business_Layer.Models;
using ServerCourseWork.Data_Access_Layer.Json;
using System.Collections.Generic;
using System.IO;

namespace ServerCourseWork.Data_Access_Layer
{
    class Db
    {
        public Dictionary<string, Laminate> Laminates { get; private set; }
        public static string connectionString;

        public Db(string path)
        {
            connectionString = path;
            Laminates = JsonOperations.DeserealizeFromFile<Dictionary<string, Laminate>>(path) ?? new();
        }
        
        public void SerializeResult() => JsonOperations.SerilalizeToFile(connectionString, Laminates);
    }
}
