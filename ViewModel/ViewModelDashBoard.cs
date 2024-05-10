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
    public class ViewModelDashBoard : ViewModelBase
    {
        #region Campos
        private readonly TaskModelDAO _taskModelDao = new TaskModelDAO();
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
        public List<TaskModel> HighPriorityTasks
        {
            get { return _highPriorityTasks; }
            set
            {
                _highPriorityTasks = value;
                OnPropertyChanged(nameof(HighPriorityTasks));
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
        #endregion campos
        public ViewModelDashBoard()
        {
            SeriesCollection = new SeriesCollection();
            TaskCompletedCollection = new SeriesCollection();

            // llamada al metodo get al crear una instancia de la VistaModelo
            GetTasksFromApi();
        }
        #region Metodos
        private async Task GetTasksFromApi() // metodo asincrono para obtener las tareas
        {
            try
            {
                TasksList = await Task.Run(() => _taskModelDao.GetAll(ViewModelBase.JwtToken));
                if (TasksList != null)
                {
                    CreatePieCharts();
                    ShowTasksInfo();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.MostrarCustomMessageBox($"Error in dashboard. Operation could not be completed. message: {ex.Message}");
            }
        }
        private void CreatePieCharts() // metodo para crear los graficos de pastel
        {
            //agrupar las tareas mediante su Priority y por la cantidad de registros
            //que posean el mismo ProrityStatus
            var tasksByPriority = TasksList.Where(x => x.Priority != null).GroupBy(t => t.Priority.PriorityStatus)
                              .Select(c => new { PriorityStatus = c.Key, Count = c.Count() });
            //agrupar las tareas mediante su TaskStatus y por la cantidad de registros
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
            //agregar los datos al grafico de pastel
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
            try
            {
                //filtrar las tareas que tengan un estado pendiente
                var pendingTasks = TasksList.Where(x => x.StatusID == 1).Reverse().ToList();
                
                //filtrar  las tareas que tengan prioridad  alta
                var highPriorityTasks = TasksList.Where(x => x.PriorityID == 3).Reverse().ToList();
                
                //ordenar las tareas por las ultimas agregadas 
                var lasTaskAdded = TasksList.ToList();
                lasTaskAdded.Reverse();
                
                //usar unicamente los primeros 3 registros
                HighPriorityTasks = highPriorityTasks.Take(3).ToList();
                LastTaskAdded = lasTaskAdded.Take(3).ToList();
                PendingTasks = pendingTasks.Take(3).ToList();
            }
            catch (Exception ex)
            {
                CustomMessageBox.MostrarCustomMessageBox($"Error in dashboard. Operation could not be completed. message: {ex.Message}");
            }
        }
        #endregion Metodos
    }
}
