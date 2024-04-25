using AdministradorDeTareas.Model;
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
        private static readonly HttpClient client = new HttpClient();
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
        private void ExecuteDeleteTask(object obj)
        {
            DeleteTaskFromApi();
        }
        public async Task DeleteTaskFromApi()
        {
            int taskId = (int)SelectedTask.TaskID;
            if (taskId == null)
            {
                taskId = 0;
            }
            string apiUrl = $"https://localhost:44384/api/Tasks/{taskId}";
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                     string Message = "Task Delete Successeflly";
                     CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                     customMessageBox.ShowDialog();
                   //  obtenemos la ventana asociada a al viewmodel actual
                    Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
                   // cerrar la ventana si se encuentra
                   if (window != null)
                   {
                       window.Close();
                   }
                }
                else
                {
                    string Message = "Task not deleted";
                    CustomMessageBox customMessageBox = new CustomMessageBox(Message);
                    customMessageBox.ShowDialog();
                    // obtenemos la ventana asociada a al viewmodel actual
                    Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
                    // cerrar la ventana si se encuentra
                    if (window != null)
                    {
                        window.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la solicitud HTTP: {ex.Message}");
            }
        }
    }
}
