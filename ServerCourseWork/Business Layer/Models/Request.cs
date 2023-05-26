namespace ServerCourseWork.Business_Layer.Models
{
    class Request
    {
        public string OperationName { get; private set; }
        public string JsonData { get; private set; }

        public Request(string operationName, string jsonData)
        {
            OperationName = operationName;
            JsonData = jsonData;
        }
    }
}
