using AdministradorDeTareas.Model;
using AdministradorDeTareas.View;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdministradorDeTareas.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        public ICommand ShowEditActionsCommand { get; }
        public ICommand ShowTaskManagmentCommand { get; }
        public ICommand ShowViewOptionsCommand { get; }
        public ICommand OpenGithubProfile {  get; }
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public MainViewModel()
        {
            //comands
            ShowViewOptionsCommand = new ViewModelCommand(ExecuteShowViewOptionsCommand);
            ShowEditActionsCommand = new ViewModelCommand(ExecuteShowEditActionsCommand);
            ShowTaskManagmentCommand = new ViewModelCommand(ExecuteShowTaskManagmentCommand);
            OpenGithubProfile = new ViewModelCommand(ExecuteOpenGithubProfile);
            //Default 
            ExecuteShowTaskManagmentCommand(null);
        }

        private void ExecuteOpenGithubProfile(object obj)
        {
            string url = "https://github.com/DanielGerardoHC";
            // abre el enlace en el navegador predeterminado del sistema
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        private void ExecuteShowEditActionsCommand(object obj)
        {
            CurrentChildView = new EditActionsModel();
        }
        private void ExecuteShowViewOptionsCommand(object obj)
        {
            CurrentChildView = new ViewModelUserAccount();
        }
        private void ExecuteShowTaskManagmentCommand(object obj)
        {
            CurrentChildView = new DashboardModel();
        }

    }
}
