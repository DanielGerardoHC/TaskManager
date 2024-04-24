using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AdministradorDeTareas.Model;
using AdministradorDeTareas.View;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ServiceModel.Channels;
using System.Windows;
using System.Text.Json.Nodes;

namespace AdministradorDeTareas.ViewModel
{
    public class EditActionsModel : ViewModelBase
    {
        private static readonly HttpClient client = new HttpClient();

        private List<TaskModel> _tasks;
        private string _txtSearch;
        public ICommand ShowAddTask { get; }
        public ICommand SearchTask { get; }
        public ICommand GetTasks { get; }  
        public List<TaskModel> TasksList
        {
            // si o si debe cada propiedad debe tener su get y set
            // junto con el metodo OnpropetyChanged
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(TasksList));
            }
        }
        public string TxtSearch
        {
            get { return _txtSearch; }
            set
            {
                _txtSearch = value;
                OnPropertyChanged(nameof(TxtSearch));
            }
        }
        public EditActionsModel()
        {
            ShowAddTask = new ViewModelCommand(ExecuteShowAddTask);
            SearchTask = new ViewModelCommand(ExecuteSearchTask);
            GetTasks = new ViewModelCommand(ExecuteGetTasks);
            GetTasksFromApi();
            TxtSearch = "";
        }
        private void ExecuteSearchTask(object obj)
        {
            if (TxtSearch != null || TxtSearch != "")
            {
                SearchTaskFromApi();
            }
        }
        private void ExecuteShowAddTask(object obj)
        {
          AddTask viewAddTask = new AddTask();
          viewAddTask.ShowDialog();
        }
        private void ExecuteGetTasks(object obj)
        {
            GetTasksFromApi();
        }
        public async Task GetTasksFromApi()
        {
            string apiUrl = "https://localhost:44384/api/Tasks";
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    TasksList = JsonConvert.DeserializeObject<List<TaskModel>>(json);
                }
                else
                {
                    MessageBox.Show($"Error al obtener las tareas. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la solicitud HTTP: {ex.Message}");
            }
        }
        public async Task SearchTaskFromApi()
        {

            string apiUrl = $"https://localhost:44384/api/Tasks?TaskName={TxtSearch}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    TasksList = JsonConvert.DeserializeObject<List<TaskModel>>(json);
                }
                else
                {
                    MessageBox.Show($"Error al obtener las tareas. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la solicitud HTTP: {ex.Message}");
            }
        }
    }
}