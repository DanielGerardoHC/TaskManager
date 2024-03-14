using AdministradorDeTareas.Model;
using AdministradorDeTareas.ViewModel;
using LiveCharts;
using LiveCharts.Wpf;
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
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection TaskCompletedCollection { get; set; }

        private int _pendingTasks;
        public int PendingTasks
        {
            get => _pendingTasks;
            set
            {
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
            // panel de la grafica
            SeriesCollection = new SeriesCollection();
            TaskCompletedCollection = new SeriesCollection();
            // Ejemplo de datos de tareas
            var tareas = new List<TaskPriorityData>
            {
                // este objeto veremos si lo podemos sustituir directamete
                // por la entidad tarea de la base de datos, en caso contrario
                // en una clase como esta almacenariamos los datos que provienen
                // de la API *Recordatorio de cambiar*
                new TaskPriorityData { Prioridad = "Bajo", Cantidad = 10 },
                new TaskPriorityData { Prioridad = "Medio", Cantidad = 20 },
                new TaskPriorityData { Prioridad = "Alto", Cantidad = 5 }
            };

            // Agregar los datos al gráfico de pastel
            foreach (var tarea in tareas)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = tarea.Prioridad,
                    Values = new ChartValues<int> { tarea.Cantidad },
                    DataLabels = true
                });
            }
            var tasks = new List<TaskCompleted> { 
               new TaskCompleted {Resolved = "completed", cantidad = 15},
               new TaskCompleted {Resolved = "Pending", cantidad = 10}
            };
            foreach (var task in tasks)
            {
                TaskCompletedCollection.Add(new PieSeries
                {
                   Title = task.Resolved,
                   Values = new ChartValues<int> { task.cantidad},
                   DataLabels = true
                });
            }
        }
        // event to INotifyPropetyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class TaskPriorityData
        {
            public string Prioridad { get; set; }
            public int Cantidad { get; set; }

        }
        public class TaskCompleted
        {
            public string Resolved { get; set; }
            public int cantidad { get; set; }
        }
    }
}
