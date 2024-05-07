using AdministradorDeTareas.Model;
using AdministradorDeTareas.Model.DAO;
using AdministradorDeTareas.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministradorDeTareas.ViewModel
{
    public class ViewModelRegister : ViewModelBase
    {
        UsersModelDAO usersModelDAO = new UsersModelDAO();
        private string _userName;
        private string _password;
        private string _email;
        private string _name;
        public string userName {
            get { return _userName; }
            set 
            { 
                  this._userName = value;
                  OnPropertyChanged(nameof(userName));
            } 
        }
        public string password { 
            get { return _password; } 
            set {
                this._password = value;
                OnPropertyChanged(nameof(password));
            } 
        }
        public string email { 
            get { return _email; } 
            set 
            { 
                this._email = value;
                OnPropertyChanged(nameof(email));
            } 
        }
        public string name { 
            get { return _name; } 
            set 
            { 
                this._name = value;
                OnPropertyChanged(nameof(name));
            } 
        }
        public ViewModelRegister() 
        {
            ReturnLoginPageCommand = new ViewModelCommand(ExecuteReturnLoginPage);       
            ShowAlertCommand = new ViewModelCommand(ExecuteShowAlert);
            RegisterCommand = new ViewModelCommand(ExecuteRegister);
        }
        public ICommand ReturnLoginPageCommand { get; }
        public ICommand ShowAlertCommand { get; }
        public ICommand RegisterCommand { get; }

        public void ExecuteReturnLoginPage(object obj)
        {
            ReturnLoginPage();
        }
        public void ExecuteShowAlert(object obj)
        {
            CustomMessageBox.MostrarCustomMessageBox("This username you will use to log in!");
        }
        public void ExecuteRegister(object obj)
        {
            Register();
        }
        public void ReturnLoginPage()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
            // cerrar la ventana si se encuentra
            if (window != null)
            {
                window.Close();
            }
        }
        public void Register()
        {
            try
            {
                UsersModel newUser = new UsersModel();
                newUser.UserName = this.userName;
                newUser.Name = this.name;
                newUser.Email = this.email;
                newUser.PasswordHash = this.password;
                if (this.password != null && this.name != null
                 && this.userName != null && this.email != null)
                {
                    if (password.Length < 6)
                    {
                        if (usersModelDAO.Post(newUser))
                        {
                            // regresamos al usuario a la vista del log in
                            LoginView loginView = new LoginView();
                            loginView.Show();
                           
                            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
                            // cerrar la ventana si se encuentra
                            if (window != null)
                            {
                                window.Close();
                            }
                        }
                    }
                    else
                    {
                        CustomMessageBox.MostrarCustomMessageBox("Password must be at least 8 characters long");
                    }
                }
                else
                {
                    CustomMessageBox.MostrarCustomMessageBox("Please fill in all registration fields");
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.MostrarCustomMessageBox("Something went wrong on the registration msg: "+ex.Message);
            }
        }
    }
}
