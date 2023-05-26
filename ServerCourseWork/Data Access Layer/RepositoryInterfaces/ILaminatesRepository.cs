using ServerCourseWork.Business_Layer.Models;
using System.Collections.Generic;

namespace ServerCourseWork.Data_Access_Layer.RepositoryInterfaces
{
    interface ILaminatesRepository
    {
        bool AddLaminate(Laminate laminate);
        bool RemoveLaminate(string name);
        bool UpdateLaminate(string name, Laminate laminate);
        Laminate GetLaminateByName(string name);
        IEnumerable<Laminate> GetAllLaminates();
        void SaveResult();
    }
}
