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
        Task<List<T>> GetAll(string token);
        Task<List<T>> GetWhere(string Title, string token);
        Task<bool> Delete(int id, string token);
        Task<bool> Post(T obj, string token);
        Task<T> GetSpecificObject(int id, string token);
        Task<bool> Put(T obj, string token);

    }
}
