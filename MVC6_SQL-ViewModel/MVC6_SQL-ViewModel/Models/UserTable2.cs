﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MVC6_SQL_ViewModel.Models
{
    public partial class UserTable2
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSex { get; set; }
        public DateTime? UserBirthDay { get; set; }
        public string UserMobilePhone { get; set; }
        public bool? UserApproved { get; set; }
        public int? DepartmentId { get; set; }

        public virtual DepartmentTable2 DepartmentTable2 { get; set; }
    }
}