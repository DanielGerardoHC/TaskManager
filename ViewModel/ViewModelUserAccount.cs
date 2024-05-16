using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AdministradorDeTareas.Model;
using AdministradorDeTareas.View;
using Microsoft.Xaml.Behaviors.Core;

namespace AdministradorDeTareas.ViewModel
{
    public class ViewModelUserAccount : ViewModelBase
    {
        private UsersModel _CurrentUser;
        public UsersModel CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        public ICommand ShowEditCredentialsCommand { get; }
        public ICommand ShowChangePasswordCommand { get; }
        public ViewModelUserAccount()
        {
            ShowEditCredentialsCommand = new ViewModelCommand(ExecuteShowEditCredentials);
            ShowChangePasswordCommand = new ViewModelCommand(ExecuteShowChangePassword);
            GetAccoutCredentirals();
        }

        private void ExecuteShowChangePassword(object obj)
        {
            ViewChangePassword viewChangePassword = new ViewChangePassword();
            viewChangePassword.ShowDialog();
        }
        private void ExecuteShowEditCredentials(object obj)
        {
            try
            {
                ViewEditUserCredentials viewEditUserCredentials = new ViewEditUserCredentials();
                ViewModelEditCredentials viewModelEditCredentials = viewEditUserCredentials.DataContext as ViewModelEditCredentials;
                if (viewModelEditCredentials != null)
                {
                    viewModelEditCredentials.AccountCredentialsEdited += GetAccoutCredentirals;
                    viewEditUserCredentials.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.MostrarCustomMessageBox("Error: Could not display the view");
            }
        }

        public void GetAccoutCredentirals()
        {
            CurrentUser = ViewModelBase.GetCurrentUser();
        }
    }

}
