﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorDeTareas.Model
{
    public class UsersModel
    {
        public int? UserID { get; set; }
        public string? UserName { get; set; }
        public String? Name {  get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
    }
}
