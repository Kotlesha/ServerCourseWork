using System.Linq;
using System.Net;

namespace ServerCourseWork.Business_Layer.Server.ServerValidation
{
    class LaminatesServerValidation : ILaminatesServerValidation
    {
        public bool CheckIpAddress(string ipAddress) =>
            ipAddress != null && ipAddress.Count(c => c == '.') == 3 && 
            IPAddress.TryParse(ipAddress, out IPAddress address);

        public bool CheckPort(int port) => port > 0;
    }
}
