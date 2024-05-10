using AdministradorDeTareas.Model;
using AdministradorDeTareas.Model.DAO;
using AdministradorDeTareas.View;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic.ApplicationServices;

namespace AdministradorDeTareas.ViewModel
{
    public class ViewModelLogIn : ViewModelBase
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
        public ICommand RegisterCommand { get; }
        public ViewModelLogIn()
        {
            LoginCommand = new ViewModelCommand(ExecuteLogin);
            RegisterCommand = new ViewModelCommand(ExecuteRegister);
        }
        public void ExecuteLogin(object obj)
        {
           Login();
        }
        public void ExecuteRegister(object obj)
        { 
            ViewRegister viewRegister = new ViewRegister();
            viewRegister.Show();
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
            // cerrar la ventana si se encuentra
            if (window != null)
            {
                window.Close();
            }
        }

        public async void Login()
        {
            if (Username != null && Password != null)
            {
                UsersModel checkUser = new UsersModel();

                checkUser.UserId = 0;
                checkUser.PasswordHash = Password;
                checkUser.UserName = Username;
                checkUser.Email = "string";
                checkUser.FullName = "string";

                string token = UserDAO.Put(checkUser);
                if (token != null)
                {
                    // si la autentificacion es correcta insertamos al usuario obtenido en el 
                    // campo statico user de ViewModelBase para que asi todos nuestros ViewModel 
                    // tengan acceso a los datos del usuario que ha iniciado sesion
                    UsersModel logUser = UserDAO.GetSpecificObject(0,token);
                    ViewModelBase.JwtToken = token;
                    ViewModelBase.user = logUser;
                    ViewMainWindow Main = new ViewMainWindow();
                    Main.Show();
                    Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
                    // cerrar la ventana si se encuentra y si la tarea se modifico con exito
                    if (window != null)
                    {
                        window.Close();
                    }
                }
            }
            else
            {
                CustomMessageBox.MostrarCustomMessageBox("Please enter valid credentials");
            }
        }

    }
}
