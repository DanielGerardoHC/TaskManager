using AdministradorDeTareas.Model;
using AdministradorDeTareas.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AdministradorDeTareas.ViewModel
{
    public class TaskManagmentModel : ViewModelBase, INotifyPropertyChanged
    {
        private int _pendingTasks;
        public int PendingTasks{
            get => _pendingTasks;
            set{
                _pendingTasks = value;
                OnPropertyChanged(nameof(PendingTasks));
            }
        }
        private int _mediumPriority;
        public int MediumPriority
        {
            get => _mediumPriority;
            set
            {
                _mediumPriority = value;
                OnPropertyChanged(nameof(MediumPriority));
            }
        }
        private int _highPriority;
        public int HighPriority
        {
            get => _highPriority;
            set
            {
                _highPriority = value;
                OnPropertyChanged(nameof(HighPriority));
            }
        }
        public TaskManagmentModel()
        {
            this.PendingTasks = 10;
            this.MediumPriority = 8;
            this.HighPriority = 12;
        }
        // event to INotifyPropetyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }  
}
