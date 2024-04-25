using AdministradorDeTareas.Model;
using AdministradorDeTareas.View;
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
    public class AddTaskViewModel : EditActionsModel
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
            get { return _title; }
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
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }
        public ICommand CreateTask { get; }
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
                    // recargamos la lista de tareas ejecutando el metodo del patre
                    // EditTaskModel

                    GetTasksFromApi();
                    string? description = "Task Added Successflly";
                    CustomMessageBox messageBox = new CustomMessageBox(description);
                    messageBox.ShowDialog();
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
                    if (Description.Length > 90 || Title.Length > 25)
                    {
                        string maxLenght = "Description should be maximum 90 characters, and title should be maximum 25 characters.";
                        CustomMessageBox messageBox = new CustomMessageBox(maxLenght);
                        messageBox.ShowDialog();
                    }
                    else
                    {
                        string badDecription = "Please fill out all fields for the task";
                        CustomMessageBox messageBox = new CustomMessageBox(badDecription);
                        messageBox.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar la tarea: " + ex.Message);
            }
        }
    }
}
