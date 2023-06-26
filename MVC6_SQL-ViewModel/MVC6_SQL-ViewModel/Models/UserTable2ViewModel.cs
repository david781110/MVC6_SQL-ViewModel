using MessagePack;

namespace MVC6_SQL_ViewModel.Models
{
    public class UserTable2ViewModel
    {

        [System.ComponentModel.DataAnnotations.Key]
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
