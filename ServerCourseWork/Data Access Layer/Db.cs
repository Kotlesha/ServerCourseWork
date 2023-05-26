using ServerCourseWork.Business_Layer.Models;
using ServerCourseWork.Data_Access_Layer.Json;
using System.Collections.Generic;

namespace ServerCourseWork.Data_Access_Layer
{
    class Db
    {
        public SortedDictionary<string, Laminate> Laminates { get; private set; }
        public static string connectionString;

        public Db(string path)
        {
            connectionString = path;
            Laminates = JsonOperations.DeserealizeFromFile<SortedDictionary<string, Laminate>>(path);
        }
        
        public void SerializeResult() => JsonOperations.SerilalizeToFile(connectionString, Laminates);
    }
}
