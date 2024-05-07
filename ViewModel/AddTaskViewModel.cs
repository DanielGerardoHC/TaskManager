using AdministradorDeTareas.Model;
using AdministradorDeTareas.Model.DAO;
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
        private static readonly TaskModelDAO taskModelDao = new TaskModelDAO();
        private int? _prioritySelect;
        private string? _description;
        private string? _title;
        private DateTime? _dueDate;

        public delegate void TaskAddedEventHandler();
        public event TaskAddedEventHandler TaskAdded;
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
        // Método para agregar una nueva tarea y disparar el evento TaskAdded
        private void AddTask()
        {
            TaskModel newTask = new TaskModel();
            newTask.StatusID = 1;
            newTask.PriorityID = (int)(PrioritySelect + 1);
            newTask.Title = Title;
            newTask.Description = Description;
            newTask.DueDate = DueDate;
            newTask.UserID = (int)ViewModelBase.user.UserID;
            newTask.TaskID = null;
            newTask.Priority = null;
            newTask.TaskStatus = null;
            newTask.Users = null;

            // llamamos al metodo de la clase DAO

            if (taskModelDao.Post(newTask))
            {
                TaskAdded?.Invoke();
                // buscamos la ventana actual
                Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
                // cerrar la ventana si se encuentra y si la tarea se modifico con exito
                if (window != null)
                {
                    window.Close();
                }
            }          
        }
        private void ExecuteCreateTask(object obj)
        {
            AddTask();
        }
    }
}
