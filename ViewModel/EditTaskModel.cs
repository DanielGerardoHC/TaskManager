 using AdministradorDeTareas.Model;
using AdministradorDeTareas.Model.DAO;
using AdministradorDeTareas.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministradorDeTareas.ViewModel
{
    public class EditTaskModel : EditActionsModel
    {
        private TaskModelDAO taskModelDAO = new TaskModelDAO();
        private int? _prioritySelect;
        private string? _description;
        private string? _title;
        private DateTime? _dueDate;
        private TaskModel _selectedTask;
        public new TaskModel  SelectedTask
        {
            get { return _selectedTask;  }
            set
            {
                _selectedTask = value;
                _selectedTask.PriorityID = value.PriorityID - 1;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
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
        public EditTaskModel()
        {
            CreateTask = new ViewModelCommand(ExecuteEditTask);
        }
        private void ExecuteEditTask(object obj)
        {
            // llamamos al metodo que ejecutara el verbo put 
            SelectedTask.PriorityID = SelectedTask.PriorityID + 1;
            MessageBox.Show($"{SelectedTask.PriorityID}");
            if (taskModelDAO.Put((int)SelectedTask.TaskID,SelectedTask))
            {
                // buscamos la ventana actual
                Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
                // cerrar la ventana si se encuentra y si la tarea se modifico con exito
                if (window != null)
                {
                    window.Close();
                }
            }

        }
    }

}
