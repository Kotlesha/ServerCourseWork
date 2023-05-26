using ServerCourseWork.Business_Layer.Models;
using ServerCourseWork.Data_Access_Layer.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerCourseWork.Data_Access_Layer.RepositoryClasses
{
    class LaminatesRepository : ILaminatesRepository
    {
        private static Db _db;

        public LaminatesRepository(string path) => _db = new(path);

        public bool AddLaminate(Laminate laminate)
        {
            if (_db.Laminates.ContainsKey(laminate.Name))
            {
                return false;
            }

            _db.Laminates.Add(laminate.Name, laminate);
            return true;
        }

        public IEnumerable<Laminate> GetAllLaminates() => _db.Laminates.Values;

        public Laminate GetLaminateByName(string name) => _db.Laminates[name];

        public bool RemoveLaminate(string name) => _db.Laminates.Remove(name);

        public void SaveResult() => _db.SerializeResult();

        public bool UpdateLaminate(string name, Laminate laminate)
        {
            if (!_db.Laminates.ContainsKey(laminate.Name))
            {
                return false;
            }

            _db.Laminates[name] = laminate;
            return true;
        }
    }
}
