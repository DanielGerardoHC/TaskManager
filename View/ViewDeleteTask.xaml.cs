﻿using AdministradorDeTareas.Model;
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
    /// Lógica de interacción para ViewDeleteTask.xaml
    /// </summary>
    public partial class ViewDeleteTask : Window
    {
        public TaskModel SelectedTask {  get; set; }
        public ViewDeleteTask(TaskModel Task)
        {
            InitializeComponent();
            this.SelectedTask = Task;
            ((DeleteTaskModel)DataContext).SelectedTask = Task;
        }
        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
