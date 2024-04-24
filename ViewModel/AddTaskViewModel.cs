using AdministradorDeTareas.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministradorDeTareas.ViewModel
{
    public class AddTaskViewModel : ViewModelBase
    {
        private static readonly HttpClient client = new HttpClient();
        private int? _prioritySelect;
        private string? _description;
        private string? _title;
        private DateTime? _dueDate;
        public int? PrioritySelect
         {
           get { return _prioritySelect; } 
           set 
            {
                _prioritySelect = value;
                OnPropertyChanged(nameof(PrioritySelect));
           } 
         }
        public string? Title
         {
            get { return _title;  }
            set
            {
                _title = value; 
                OnPropertyChanged(nameof(Title));
            }
         }
        public string? Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));

            }
        }     
        public DateTime? DueDate
        {
            get { return _dueDate; }
            set { 
                _dueDate = value; 
                 OnPropertyChanged(nameof(DueDate));
            }
        }
        public ICommand CreateTask {  get; }
        public AddTaskViewModel()
        {
            CreateTask = new ViewModelCommand(ExecuteCreateTask);
        }
        private void ExecuteCreateTask(object obj)
        {
            CreateNewTask();
        }
        private async Task CreateNewTask()
        {
            try
            {
                // instanciamos una nueva instancia de la clase TaskModel
                TaskModel newTask = new TaskModel();
                newTask.StatusID = 1;
                newTask.PriorityID = PrioritySelect + 1;
                newTask.Title = Title;
                newTask.Description = Description;
                newTask.DueDate = DueDate;
                newTask.UserID = 16;
                newTask.TaskID = null;
                newTask.Priority = null;
                newTask.TaskStatus = null;
                newTask.Users = null;

                string JsonTask = JsonConvert.SerializeObject(newTask);
                string urlApi = "https://localhost:44384/api/Tasks";
                // configurar la solicitud HTTP POST con el contenido JSON
                HttpResponseMessage response = await client.PostAsync(urlApi, new StringContent(JsonTask, Encoding.UTF8, "application/json"));
                // verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("La tarea se creó exitosamente.");
                }
                else
                {
                    MessageBox.Show($"Error al crear la tarea: {response.StatusCode}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al ingresar la tarea: " + ex.Message);
            }
        }
    }
}
