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
using AdministradorDeTareas.Model.DAO;
using static System.Net.WebRequestMethods;

namespace AdministradorDeTareas.ViewModel
{
    public class EditActionsModel : ViewModelBase
    {
        #region Atributos
        private static TaskModelDAO taskModelDAO = new TaskModelDAO();
        private List<TaskModel> _tasks;
        private string _txtSearch;
        private TaskModel _selectedTask { get; set; }
        public ICommand ShowEditTask { get;  }
        public ICommand ShowAddTask { get; }
        public ICommand SearchTask { get; }
        public ICommand GetTasks { get; }  
        public ICommand ShowViewDeleteTask {  get; }
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
        public TaskModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }

        }
        #endregion
        public EditActionsModel()
        {
            // inicializamos todos los comandos que usaremos
            ShowAddTask = new ViewModelCommand(ExecuteShowAddTask);
            SearchTask = new ViewModelCommand(ExecuteSearchTask);
            GetTasks = new ViewModelCommand(ExecuteGetTasks);
            ShowViewDeleteTask = new ViewModelCommand(ExecuteShowViewDeleteTask);
            ShowEditTask = new ViewModelCommand(ExecuteShowEditTask);
            // llenamos el listbox llamando al verbo get
            GetAllTasks();
        }

        #region MetodosDeComandos
        private void ExecuteSearchTask(object obj)
        {
            if (TxtSearch != null || TxtSearch != "")
            {
                TasksList = taskModelDAO.GetWhere("https://localhost:44384/api/Tasks?TaskName=",TxtSearch);
            }
        }
        private void ExecuteShowAddTask(object obj)
        {
            ViewAddTask viewAddTask = new ViewAddTask();
            viewAddTask.ShowDialog();
        }
        private void ExecuteGetTasks(object obj)
        {
            GetAllTasks();
        }
        private void ExecuteShowEditTask(object obj)
        {
            ViewEditTask viewEditTask = new ViewEditTask(SelectedTask);
            viewEditTask.ShowDialog();
        }
        public void ExecuteShowViewDeleteTask(object obj)
        {
            ViewDeleteTask viewDeleteTask = new ViewDeleteTask(SelectedTask);
            viewDeleteTask.ShowDialog();
        }

        public async Task GetAllTasks()
        {
            TasksList = taskModelDAO.GetAll("https://localhost:44384/api/Tasks");
        }
        #endregion
    }
}