﻿using System.ComponentModel.DataAnnotations;

namespace MVC6_SQL_ViewModel.Models
{
    public class UserDepartmentViewModel
    {
        [Key]
        public UserTable2 UserVM { get; set; }

        public DepartmentTable2 DepartmentVM { get; set; }
    }
}
