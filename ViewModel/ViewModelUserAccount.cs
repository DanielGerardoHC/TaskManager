using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AdministradorDeTareas.Model;

namespace AdministradorDeTareas.ViewModel
{
    public class ViewModelUserAccount : ViewModelBase
    {
        public UsersModel currentUser { get; set; }

        public ViewModelUserAccount()
        {
            currentUser = ViewModelBase.user;
        }
    }

}
