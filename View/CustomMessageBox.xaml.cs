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
    /// Lógica de interacción para CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public string? description {  get; set; }
        public CustomMessageBox(string? description="Action Completed")
        {
            InitializeComponent();
            this.description = description;
            DataContext = this;
        }
        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
