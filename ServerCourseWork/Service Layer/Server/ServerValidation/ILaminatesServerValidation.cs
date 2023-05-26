namespace ServerCourseWork.Service_Layer.Server.ServerValidation
{
    interface ILaminatesServerValidation
    {
        bool CheckIpAddress(string ipAddress);
        bool CheckPort(int port);
    }
}
