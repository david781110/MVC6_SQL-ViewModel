namespace MVC6_SQL_ViewModel.Models
{
    public class UDViewModel
    {
        // *** 一對多 ***。     
        //       一個「科系」可以對應多名「學生」
        public DepartmentTable2 DVM { get; set; }

        public IList<UserTable2> UVM { get; set; }


        //      *******
        // 比較一下，跟另一個 ViewModel有何不同？（另一個為 UserDepartmentViewModel.cs，一對一）
        //                                                                       （另一個為 UDmultiViewModel.cs，多對多）

    }
}
