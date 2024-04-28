using AdministradorDeTareas.Model;
using AdministradorDeTareas.Model.DAO;
using AdministradorDeTareas.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministradorDeTareas.ViewModel
{
    public class DeleteTaskModel : EditActionsModel
    {
        private static readonly TaskModelDAO taskModelDAO = new TaskModelDAO();
        private TaskModel _selectedTask;
        public TaskModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        public ICommand DeleteTask { get; }
        public DeleteTaskModel()
        {
            DeleteTask = new ViewModelCommand(ExecuteDeleteTask);
        }

        // metodo para eliminar tarea
        private void ExecuteDeleteTask(object obj)
        {
            DeleteTaskAsync();
        }
        public async void DeleteTaskAsync()
        {
            int taskID = (int)SelectedTask.TaskID;
            if (taskModelDAO.Delete(taskID))
            {
                // buscamos la ventana actual
                Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
                // cerrar la ventana si se encuentra y si la tarea se modifico con exito
                if (window != null)
                {
                    window.Close();
                }
            }
            TasksList = taskModelDAO.GetAll("https://localhost:44384/api/Tasks");
        }
    }
}
