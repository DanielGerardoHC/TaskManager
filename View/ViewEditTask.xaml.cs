using AdministradorDeTareas.Model;
using AdministradorDeTareas.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdministradorDeTareas.View
{
    /// <summary>
    /// Lógica de interacción para ViewEditTask.xaml
    /// </summary>
    public partial class ViewEditTask : Window
    {

        public TaskModel selectedTask {  get; set; }
        public ViewEditTask(TaskModel Task)
        {
            InitializeComponent();
            this.selectedTask = Task;
            ((EditTaskModel)DataContext).SelectedTask = Task;

        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
