using AdministradorDeTareas.Model;
using AdministradorDeTareas.Model.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministradorDeTareas.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        UsersModelDAO UserDAO = new UsersModelDAO();
        private string _password;
        private string _username;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand LoginCommand { get; }
        
        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLogin);
        }
        public void ExecuteLogin(object obj)
        {
            UsersModel checkUser = new UsersModel();

            checkUser.PasswordHash = Password;
            checkUser.UserName = Username;
            checkUser.Email = "default";
            checkUser.UserID = -1;
            checkUser.Name = "deafult";

             UsersModel isValisUser = UserDAO.Put(checkUser);
             if(isValisUser != null)
            {
                // si la autentificacion es correcta insertamos al usuario obtenido en el 
                // campo user de ViewModelBase para que asi todos nuestros ViewModel 
                // tengan acceso a los datos del usuario que ha iniciado sesion
                ViewModelBase.user = isValisUser;
                MainWindow Main = new MainWindow();
                Main.Show();
                Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
                // cerrar la ventana si se encuentra y si la tarea se modifico con exito
                if (window != null)
                {
                    window.Close();
                }
             }
        }

    }
}
