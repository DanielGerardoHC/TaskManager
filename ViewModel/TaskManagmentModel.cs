using AdministradorDeTareas.Model;
using AdministradorDeTareas.ViewModel;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.Json.Nodes;
using System.Windows;
using LiveCharts.Helpers;

namespace AdministradorDeTareas.ViewModel
{
    public class TaskManagmentModel : ViewModelBase
    {
        private static readonly HttpClient client = new HttpClient();
        private List<TaskModel> _tasks;
        private List<TaskModel> _pendingTasks;
        private List<TaskModel> _highPriorityTasks;
        private List<TaskModel> _lastTaskAdded;
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
        public List<TaskModel> PendingTasks
        {
            get { return _pendingTasks; }
            set
            {
                _pendingTasks = value;
                OnPropertyChanged(nameof(PendingTasks));
            }
        }
        public List<TaskModel> HighPrirityTasks
        {
            get { return _highPriorityTasks; }
            set
            {
                _highPriorityTasks = value;
                OnPropertyChanged(nameof(HighPrirityTasks));
            }
        }
        public List<TaskModel> LastTaskAdded
        {
            get { return _lastTaskAdded; }
            set
            {
                _lastTaskAdded = value;
                OnPropertyChanged(nameof(LastTaskAdded));
            }
        }        
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection TaskCompletedCollection { get; set; }
        public TaskManagmentModel()
        {
            SeriesCollection = new SeriesCollection();
            TaskCompletedCollection = new SeriesCollection();

            //end point para obtener todas las tareas y luego llenar los PieChart
            GetTasksFromApi();
        }
        private async void GetTasksFromApi()
        {
            string apiUrl = "https://localhost:44384/api/Tasks";
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    TasksList = JsonConvert.DeserializeObject<List<TaskModel>>(json);

                    // Después de llenar TasksList, crear las series para los gráficos
                    CreatePieCharts();
                    ShowTasksInfo();
                }
                else
                {
                    MessageBox.Show($" Error al tratar de obtener los datos: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Error en la solicitud HTTPS: {ex.Message}");
            }
        }
        private void CreatePieCharts()
        {
            //agrupamos las tareas mediante su Priority y por la cantidad de registros
            //que posean el mismo ProrityStatus
            var tasksByPriority = TasksList.GroupBy(t => t.Priority.PriorityStatus)
                              .Select(c => new { PriorityStatus = c.Key, Count = c.Count() });
            //agrupamos las tareas mediante su TaskStatus y por la cantidad de registros
            //que posean el mismo StatusName
            var tasksByStatus = TasksList.GroupBy(t => t.TaskStatus.StatusName)
                                 .Select(t => new { StatusName = t.Key, Count = t.Count() });

            //agregar los datos al gráfico de pastel
            foreach (var Group in tasksByStatus)
            {
                TaskCompletedCollection.Add(new PieSeries
                {
                    Title = Group.StatusName,
                    Values = new ChartValues<int> { Group.Count }
                });
            }
            foreach (var Group in tasksByPriority)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = Group.PriorityStatus,
                    Values = new ChartValues<int> { Group.Count }
                });
            }
        }
        private void ShowTasksInfo()
        { 
            // filtramos las tareas que tengan un estado pendiente
            var PendingTasks_aux = TasksList.Where(x => x.TaskStatus.StatusName == "Pending").Reverse();
            var HighPriorityTasks_aux = TasksList.Where(x => x.Priority.PriorityStatus == "High").Reverse();
            var LasTaskAdded_aux = TasksList.ToList();
            LasTaskAdded_aux.Reverse();
            HighPrirityTasks = HighPriorityTasks_aux.Take(3).ToList();
            LastTaskAdded = LasTaskAdded_aux.Take(3).ToList();
            PendingTasks = PendingTasks_aux.Take(3).ToList(); // usaremos unicamente los primeros 5 registros
        }
        
    }
}
