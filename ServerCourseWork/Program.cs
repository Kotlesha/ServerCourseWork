using ServerCourseWork.Service_Layer;
using ServerCourseWork.Service_Layer.Server;
using System.Threading.Tasks;

namespace ServerCourseWork
{
    class Program
    {
        private static readonly string connectionString = "laminates.json";
        private static readonly string ipAddress = "127.0.0.1";
        private static readonly int port = 80;

        static async Task Main(string[] args)
        {
            LaminatesService laminatesService = new(connectionString);
            var laminatesServer = new LaminatesServer(ipAddress, port, laminatesService);
            await laminatesServer.StartAsync();
        }
    }
}
