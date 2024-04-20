using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdministradorDeTareas.ViewModel
{
    public class AddTaskViewModel : ViewModelBase
    {
        private DateTime? _dueDate;
        public DateTime? DueDate
        {
            get { return _dueDate; }
            set { SetProperty(ref _dueDate, value); }
        }

        public ICommand AddTaskCommand { get; }

        public AddTaskViewModel()
        {
            AddTaskCommand = new DelegateCommand(AddTask, CanAddTask);
        }

        private bool CanAddTask(object parameter)
        {
            // Lógica de validación para determinar si se puede agregar una tarea
            return DueDate.HasValue; // Por ejemplo, requerimos una fecha de vencimiento
        }

        private void AddTask(object parameter)
        {
            // Lógica para agregar la tarea con la fecha de vencimiento seleccionada
            // Puedes acceder a la fecha de vencimiento a través de la propiedad DueDate
        }
    }
}
