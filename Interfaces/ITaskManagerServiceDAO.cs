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
        List<T> GetAll(string token);
        List<T> GetWhere(string Title, string token);
        bool Delete(int id, string token);
        bool Post(T obj, string token);
        T GetSpecificObject(int id, string token);
        public bool Put(T obj, string token);

    }
}
