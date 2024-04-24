﻿using AdministradorDeTareas.Model;
using AdministradorDeTareas.View;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
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

            //Default 
            ExecuteShowTaskManagmentCommand(null);
        }

        private void ExecuteShowEditActionsCommand(object obj)
        {
            CurrentChildView = new EditActionsModel();
        }
        private void ExecuteShowViewOptionsCommand(object obj)
        {
            CurrentChildView = new ViewOptionsModel();
        }
        private void ExecuteShowTaskManagmentCommand(object obj)
        {
            CurrentChildView = new TaskManagmentModel();
        }
    }
}