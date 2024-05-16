using System.Windows.Input;
using AdministradorDeTareas.Model;
using AdministradorDeTareas.Model.DAO;
using System.Windows;
using AdministradorDeTareas.View;

namespace AdministradorDeTareas.ViewModel;

public class ViewModelEditCredentials : ViewModelUserAccount
{
    private readonly UsersModelDAO _usersModelDao = new UsersModelDAO();
    public delegate void TaskDeletedEventHandler();
    public event TaskDeletedEventHandler AccountCredentialsEdited;
    public UsersModel CurrentUser { get; set; }
    public ICommand EditCredentialsCommand { get; }
    public ICommand CancelEditCommand { get; }

    public ViewModelEditCredentials()
    {
        CurrentUser = new UsersModel();
        CurrentUser = ViewModelBase.GetCurrentUser();
        EditCredentialsCommand = new ViewModelCommand(ExecuteEditCredentials);
        CancelEditCommand = new ViewModelCommand(ExecuteCancelEdit);
    }

    private void ExecuteEditCredentials(object obj)
    {
        EditCredentials();
    }

    private void ExecuteCancelEdit(object obj)
    {
        Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
        //cerrar la ventana si se encuentra y si la tarea se modifico con exito
        if (window != null)
        {
            window.Close();
        } 
    }
    private async void EditCredentials()
    {
        if (await _usersModelDao.Put(CurrentUser, ViewModelBase.JwtToken))
        {
            ViewModelBase.SetCurrentUser(await _usersModelDao.GetSpecificObject((int)ViewModelBase.GetCurrentUser().UserId,ViewModelBase.JwtToken));
            AccountCredentialsEdited.Invoke();
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
            //cerrar la ventana si se encuentra y si la tarea se modifico con exito
            if (window != null)
            {
                window.Close();
            }
        }
    }
    
    
}