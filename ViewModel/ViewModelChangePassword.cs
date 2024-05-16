using System.Windows.Input;
using AdministradorDeTareas.Model;
using AdministradorDeTareas.Model.DAO;
using System.Windows;

namespace AdministradorDeTareas.ViewModel;

public class ViewModelChangePassword : ViewModelBase
{
    private readonly UsersModelDAO _usersModelDao = new UsersModelDAO();
    private string? _OldPassword;
    private string? _NewPassword;
    public string OldPassword
    {
        get { return _OldPassword; }
        set
        {
            _OldPassword = value;
            OnPropertyChanged(nameof(OldPassword));
        }
    }
    public string NewPassword
    {
        get { return _NewPassword; }
        set
        {
            _NewPassword = value;
            OnPropertyChanged(nameof(NewPassword));
        }
    }
    public ICommand ChangePasswordCommand { get; }
    public ICommand CancelChangePassword { get; }
    public ViewModelChangePassword()
    {
        ChangePasswordCommand = new ViewModelCommand(ExecuteChangePasswordCommand);
        CancelChangePassword = new ViewModelCommand(ExecuteCancelChangePassword);
    }

    private void ExecuteChangePasswordCommand(object obj)
    {
        ChangePassword();
    }

    private void ExecuteCancelChangePassword(object obj)
    {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
            //cerrar la ventana si se encuentra y si la tarea se modifico con exito
            if (window != null)
            {
                window.Close();
            } 
    }
    private async void ChangePassword()
    {

        if (await _usersModelDao.ChangePass(
                new ChangePasswordModel
                {
                    UserId = (int)ViewModelBase.GetCurrentUser().UserId,
                    UserName = ViewModelBase.GetCurrentUser().UserName,
                    OldPassword = OldPassword,
                    NewPassword = NewPassword
                },
                ViewModelBase.JwtToken))
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.DataContext == this);
            if (window != null)
            {
                window.Close();
            }
        }
    }
    
    
}