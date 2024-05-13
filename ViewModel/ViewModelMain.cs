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
    public class ViewModelMain : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        public ICommand ShowEditActionsCommand { get; }
        public ICommand ShowDashBoardCommand { get; }
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
        
        public ViewModelMain()
        {
            //comands
            ShowViewOptionsCommand = new ViewModelCommand(ExecuteShowViewOptionsCommand);
            ShowEditActionsCommand = new ViewModelCommand(ExecuteShowEditActionsCommand);
            ShowDashBoardCommand = new ViewModelCommand(ExecuteShowDashBoardCommand);
            OpenGithubProfile = new ViewModelCommand(ExecuteOpenGithubProfile);
            //Default 
            ExecuteShowDashBoardCommand(null);
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
            CurrentChildView = new ViewModelEditActions();
        }
        private void ExecuteShowViewOptionsCommand(object obj)
        {
            CurrentChildView = new ViewModelUserAccount();
        }
        private void ExecuteShowDashBoardCommand(object? obj)
        {
            CurrentChildView = new ViewModelDashBoard();
        }

    }
}
