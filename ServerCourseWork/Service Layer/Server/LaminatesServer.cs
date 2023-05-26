using ServerCourseWork.Business_Layer.Models;
using ServerCourseWork.Data_Access_Layer.Json;
using ServerCourseWork.Service_Layer.Server.ServerValidation;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCourseWork.Service_Layer.Server
{
    class LaminatesServer
    {
        private readonly IPAddress _ipAddress;
        private readonly int _port;
        private readonly LaminatesService _laminatesService;
        private TcpListener _tcpListener;
        
        public LaminatesServer(string ipAddress, int port, LaminatesService laminatesService)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            LaminatesServerValidation laminatesServerValidation = new();
            Console.ForegroundColor = ConsoleColor.Red;

            if (!laminatesServerValidation.CheckIpAddress(ipAddress))
            {
                Console.WriteLine("Произошла ошибка! Неккоректный IPv4-адрес!");
                ipAddress = "127.0.0.1";
                Console.WriteLine($"Текущий ip-адрес: {ipAddress}");
            }

            if (!laminatesServerValidation.CheckPort(port))
            {
                Console.WriteLine("Произошла ошибка! Неккоректный порт!");
                port = 80;
                Console.WriteLine($"Текущий порт: {port}");
            }

            _ipAddress = IPAddress.Parse(ipAddress);
            _port = port;
            _laminatesService = laminatesService;
            _tcpListener = new(_ipAddress, _port);
        }

        private void OnProcessExit(object sender, EventArgs e) => Stop();

        public async Task StartAsync()
        {
            _tcpListener.Start();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Добро пожаловать на сервер!");
            Console.WriteLine($"IPv4-адрес: {_ipAddress}, порт: {_port}");
            Console.WriteLine("Ожидание подключений...");

            while (true)
            {
                TcpClient tcpClient = await _tcpListener.AcceptTcpClientAsync();
                Console.WriteLine("Клиент подключен!");
                ProcessDataAsync(tcpClient);
            }
        }

        private async void ProcessDataAsync(TcpClient tcpClient)
        {
            using (tcpClient)
            {
                var stream = tcpClient.GetStream();
                var buffer = new byte[160000];

                while (true)
                {
                    var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length));

                    if (bytesRead == 0)
                    {
                        _laminatesService.LaminatesRepository.SaveResult();
                        break;
                    }

                    string jsonRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var request = JsonOperations.Deserealize<Request>(jsonRequest);
                    string result = GetResult(request);

                    if (result.Equals("End"))
                    {
                        _laminatesService.LaminatesRepository.SaveResult();
                        break;
                    }

                    var resultBytes = Encoding.UTF8.GetBytes(result);
                    await stream.WriteAsync(resultBytes.AsMemory(0, resultBytes.Length));
                }
            }

            Console.WriteLine("Клиент отключен!");
        }

        private string GetResult(Request request)
        {
            string result;

            switch (request.OperationName)
            {
                case "AddLaminate":
                    Laminate laminate = JsonOperations.Deserealize<Laminate>(request.JsonData);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Добавление ламината:\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(laminate);
                    bool resultOperation = _laminatesService.LaminatesRepository.AddLaminate(laminate);
                    result = resultOperation ? "Ламинат добавлен успешно!" : "Такой вид ламината уже есть!";
                    Console.WriteLine(result);
                    break;
                case "RemoveLaminate":
                    string laminateName = JsonOperations.Deserealize<string>(request.JsonData);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Удаление ламината, который имеет название: {laminateName}");
                    resultOperation = _laminatesService.LaminatesRepository.RemoveLaminate(laminateName);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    result = resultOperation ? "Ламинат удалён успешно!" : "Такого вида ламината не существует!";
                    Console.WriteLine(result);
                    break;
                case "UpdateLaminate":
                    var data = JsonOperations.Deserealize<(string, Laminate)>(request.JsonData);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Обновление ламината, который имеет название: {data.Item1}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    resultOperation = _laminatesService.LaminatesRepository.UpdateLaminate(data.Item1, data.Item2);
                    Console.WriteLine($"Текущий ламинат будет иметь следующие данные: {data.Item2}");
                    result = resultOperation ? "Информация о ламинате обновлена успешно!" : "Такого вида ламината не существует!";
                    Console.WriteLine(result);
                    break;
                case "GetLaminateByName":
                    laminateName = JsonOperations.Deserealize<string>(request.JsonData);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Получение информации о ламинате с названием: {laminateName}");
                    laminate = _laminatesService.LaminatesRepository.GetLaminateByName(laminateName);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(laminate);
                    result = JsonOperations.Serilalize(laminate);
                    break;
                case "GetAllLaminates":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    var allLaminates = _laminatesService.LaminatesRepository.GetAllLaminates();
                    Console.WriteLine($"Получение всего списка ламината:");
                    Console.ForegroundColor = ConsoleColor.Blue;

                    if (!allLaminates.Any())
                    {
                        Console.WriteLine("Информация отсутствует!");
                    }
                    else
                    {
                        Console.WriteLine(string.Join("\n", allLaminates));
                    }

                    result = JsonOperations.Serilalize(allLaminates);
                    break;
                case "End connection":
                    result = "End";
                    break;
                default:
                    result = "Неизвестный метод";
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            return result;
        }

        public void Stop()
        {
            _tcpListener.Stop();
            _laminatesService.LaminatesRepository.SaveResult();
        }
    }
}
