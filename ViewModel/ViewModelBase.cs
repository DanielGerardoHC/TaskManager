﻿using AdministradorDeTareas.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using Microsoft.VisualBasic.ApplicationServices;

namespace AdministradorDeTareas.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private static UsersModel? CurrentUser;
        protected static string? JwtToken { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static void SetCurrentUser(UsersModel user)
        { 
            CurrentUser = user;
        }
        public static UsersModel GetCurrentUser()
        {
            return new UsersModel()
            {
                UserName = CurrentUser.UserName,
                FullName = CurrentUser.FullName,
                UserId = CurrentUser.UserId,
                Email = CurrentUser.Email
            };
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
