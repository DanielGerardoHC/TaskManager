using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdministradorDeTareas.Interfaces
{
    interface ITaskManagerServiceDAO<T> where T : class
    {
        List<T> GetAll(int userID);
        List<T> GetWhere(string Title, int userID);
        bool Delete(int id);
        bool Post(T obj);
        T GetEspecificObject(int id, int userID);
        public bool Put(int id ,T obj);

    }
}
