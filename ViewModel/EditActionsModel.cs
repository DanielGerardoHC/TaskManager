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
            GetTaskWhere();
        }
        private void RechargeTaskList()
        {
            GetAllTasks();
        }
        private void ExecuteShowAddTask(object obj)
        {
            // Creamos una instancia de la vista viewAddTask
            ViewAddTask viewAddTask = new ViewAddTask();
            AddTaskViewModel addTaskModel = viewAddTask.DataContext as AddTaskViewModel;
            if (addTaskModel != null)
            {
                // enviamos el metodo RechargeTaskList al delegado del viewModel de
                // la vista viewAddTask
                addTaskModel.TaskAdded += RechargeTaskList;
                viewAddTask.ShowDialog();
            }
        }
        private void ExecuteGetTasks(object obj)
        {
            GetAllTasks();
        }
        private void ExecuteShowEditTask(object obj)
        {
            if (SelectedTask != null)
            {
                ViewEditTask viewEditTask = new ViewEditTask(SelectedTask);
                EditTaskModel editTaskModel = viewEditTask.DataContext as EditTaskModel;
                if (editTaskModel != null)
                {
                    editTaskModel.TaskEdited += RechargeTaskList;
                    viewEditTask.ShowDialog();
                }
            }
            else
            {
                CustomMessageBox.MostrarCustomMessageBox("Please select a task");
            }
            
        }
        public void ExecuteShowViewDeleteTask(object obj)
        {
            if (SelectedTask != null)
            {
                ViewDeleteTask viewDeleteTask = new ViewDeleteTask(SelectedTask);
                // Asignacion del viewModel como contexto de datos para la vista ViewDeleteTask
                DeleteTaskModel deleteTaskModel = viewDeleteTask.DataContext as DeleteTaskModel;
                if (deleteTaskModel != null)
                {
                    // asignacion del metodo RechargeTaskList al delegado de la VistaModelo
                    deleteTaskModel.TaskDeleted += RechargeTaskList;
                    viewDeleteTask.ShowDialog();
                }
            }
            else
            {
                CustomMessageBox.MostrarCustomMessageBox("Please select a task");
            }
        }
        public async Task GetTaskWhere()
        {
            if (TxtSearch != null || TxtSearch != "")
            {
                TasksList = await Task.Run(() => taskModelDAO.GetWhere(TxtSearch, ViewModelBase.JwtToken));
            }
        }
        public async Task GetAllTasks()
        {
            TasksList = await Task.Run(() => taskModelDAO.GetAll(ViewModelBase.JwtToken));
        }
        #endregion
    }
}