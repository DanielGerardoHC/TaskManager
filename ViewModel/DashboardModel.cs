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
using AdministradorDeTareas.Model.DAO;
using AdministradorDeTareas.View;

namespace AdministradorDeTareas.ViewModel
{
    public class DashboardModel : ViewModelBase
    {
        private static readonly HttpClient client = new HttpClient();
        private TaskModelDAO  taskModelDAO = new TaskModelDAO();
        private List<TaskModel>? _tasks;
        private List<TaskModel>? _pendingTasks;
        private List<TaskModel>? _highPriorityTasks;
        private List<TaskModel>? _lastTaskAdded;
        public List<TaskModel> TasksList
        { 
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
        public DashboardModel()
        {
            SeriesCollection = new SeriesCollection();
            TaskCompletedCollection = new SeriesCollection();

            // llamamos al metodo que ara la consulta get a la Api
            GetTasksFromApi();
        }
        // metodo para ejecutar el verbo get de la api y mostrar las estadisticas
        private async Task GetTasksFromApi()
        {
            try
            {
                TasksList = await Task.Run(() => taskModelDAO.GetAll((int)ViewModelBase.user.UserID));
                if (TasksList != null)
                {
                    CreatePieCharts();
                    ShowTasksInfo();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error: Operation could not be completed. message: {ex.Message}";
                CustomMessageBox customMessageBox = new CustomMessageBox(errorMessage);
                customMessageBox.ShowDialog();
            }
        }
        private void CreatePieCharts()
        {
            //agrupamos las tareas mediante su Priority y por la cantidad de registros
            //que posean el mismo ProrityStatus
            var tasksByPriority = TasksList.Where(x => x.Priority != null).GroupBy(t => t.Priority.PriorityStatus)
                              .Select(c => new { PriorityStatus = c.Key, Count = c.Count() });
            //agrupamos las tareas mediante su TaskStatus y por la cantidad de registros
            //que posean el mismo StatusName
            var tasksByStatus = TasksList.Where(x => x.TaskStatus != null).GroupBy(t => t.TaskStatus.StatusName)
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
            var PendingTasks_aux = TasksList.Where(x => x.TaskStatus.StatusName == "Pendiente" ).Reverse();
            var HighPriorityTasks_aux = TasksList.Where(x => x.Priority != null).Where(x => x.Priority.PriorityStatus == "High" ).Reverse();
            var LasTaskAdded_aux = TasksList.ToList();
            LasTaskAdded_aux.Reverse();
            // usaremos unicamente los primeros 3 registros
            HighPrirityTasks = HighPriorityTasks_aux.Take(3).ToList();
            LastTaskAdded = LasTaskAdded_aux.Take(3).ToList();
            PendingTasks = PendingTasks_aux.Take(3).ToList(); 
        }
        
    }
}
