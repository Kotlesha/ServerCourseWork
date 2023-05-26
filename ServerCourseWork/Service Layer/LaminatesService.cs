using ServerCourseWork.Data_Access_Layer.RepositoryClasses;
using ServerCourseWork.Data_Access_Layer.RepositoryInterfaces;

namespace ServerCourseWork.Service_Layer
{
    class LaminatesService
    {
        public ILaminatesRepository LaminatesRepository { get; private set; }

        public LaminatesService(string path) => LaminatesRepository = new LaminatesRepository(path);
    }
}
