using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC6_SQL_ViewModel.Models;
using SQLitePCL;

namespace MVC6_SQL_ViewModel.Controllers
{
    public class UserDB2Controller : Controller
    {
        private readonly MVC_UserDB2Context _db;

        public UserDB2Controller(MVC_UserDB2Context context)
        {
            _db = context;
        }

        // GET: UserDB2
        public IActionResult Index()
        {
            //******************************************
            //（錯誤）  // return View(_db.UserTable2s);  // 以前 .NET 4.x版MVC、EF的作法，在.NET Core出問題。透過SQL Profiler觀察，沒有做JOIN。
            // 必須改寫如下：
            var UserTable2 = _db.UserTable2.Include(x => x.DepartmentTable2);
            return View(UserTable2);
            // 改寫以後，SQL Profiler的成果如下：
            //  SELECT[u].[UserId], [u].[DepartmentId], [u].[UserApproved], [u].[UserBirthDay], [u].[UserMobilePhone], [u].[UserName], [u].[UserSex], [d].[DepartmentId], [d].[DepartmentName], [d].[DepartmentYear]
            // FROM[UserTable2] AS[u]
            // LEFT JOIN[DepartmentTable2] AS[d] ON[u].[DepartmentId] = [d].[DepartmentId]
        }


        // *** [簡易版] 內容展示 -- 主表明細（Master Details / 主細表） ***
        //  一對多（一個科系有幾位學生？）
        public IActionResult IdexMasterDetails(int _ID = 1)
        {


            //var IdexMasterDetails = from m in _db.DepartmentTable2
            //                        join p in _db.UserTable2 on m.DepartmentId equals p.DepartmentId
            //                        where m.DepartmentId == _ID
            //                        select m;

            var IdexMasterDetails = _db.DepartmentTable2.Include(x => x.UserTable2).Where(x => x.DepartmentId == _ID).FirstOrDefault();
            return View(IdexMasterDetails);
        }

        //==============================================================================
        // 以 UserTable2s（學生）為主角。    .Include() 類似 SQL指令的 Join
        // 以前的MVC與EF，翻譯成 Inner Join。現在的 .NET Core / EF Core 翻譯成LEFT JOIN
        public IActionResult IdexMasterDetails2()  // 傳回多筆數據，檢視畫面請使用 List範本
        {

            // .Include() 請輸入字串，UserTable2s類別裡面，導覽屬性的「名稱」
            // 第一種作法：傳回多筆數據，檢視畫面請使用 List範本
            var result = _db.UserTable2.Include(x => x.DepartmentTable2).OrderBy(x => x.DepartmentId);
            // SQL Profile翻譯後的成果 ---- 以前的MVC與EF，翻譯成 Inner Join。現在的 .NET Core / EF Core 翻譯成LEFT JOIN
            
            // SELECT[u].[UserId], [u].[DepartmentId], [u].[UserApproved], [u].[UserBirthDay], [u].[UserMobilePhone], [u].[UserName], [u].[UserSex], [d].[DepartmentId], [d].[DepartmentName], [d].[DepartmentYear]
            // FROM[UserTable2] AS[u]
            // LEFT JOIN[DepartmentTable2] AS[d] ON[u].[DepartmentId] = [d].[DepartmentId]
            // ORDER BY[u].[DepartmentId]
            return View(result);
        }

        // 以 UserTable2s（學生）為主角。  作法同上，透過 UserTable2s類別的「導覽屬性」來串連資料
        public IActionResult IdexMasterDetails2_Navigation(int _ID = 1) // 給 _ID變數（科系ID），一個預設值
        {   // 透過 UserTable2s類別的「導覽屬性」來串連資料

            ////第三種作法。  輸出為「匿名類型」
            var result = from ut in _db.UserTable2
                         where ut.DepartmentId == _ID
                         select new 
                         {
                             //輸出結果，重新命名
                             uID = ut.UserId,
                             uName = ut.UserName,
                             uSex = ut.UserSex,
                             uBirthDay = ut.UserBirthDay,
                             uDepartmentId = ut.DepartmentId,
                             DepartmentTable2 = ut.DepartmentTable2,
                         };
            // ******************** 重點！！ UserTable2s的「導覽屬性」對應 -- 科系（Department2s）資料表
            // SQL Profiler翻譯後的成果 ，放在下一個動作裡。

            // 把結果（ "一對多"的列表）呈現出來。
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            foreach(var item in result)
            // 重點！！ UserTable2s的「導覽屬性」對應 -- 科系（Department2s）資料表
            {
                sb.Append("<h3>科系ID -- " + item.DepartmentTable2.DepartmentId + " -- "
                                                               + item.DepartmentTable2.DepartmentName + "</h3>");   // 科系ID（DepartmentId）
                //                                                            // *****************重點！！

                sb.Append("<br />========================<br />");
                sb.Append("<br />學生ID -- " + item.uID + " -- " + item.uName + " -- （科系ID）" + item.uDepartmentId);
                //                                                               //***** 改用 select new { } 裡面的新名字 uID與 uNAME與 uDEPARTMENTID
            }

            return Content(sb.ToString());
            // 請使用「List」範本。從上一個檢視畫面來改，使用的「模型類別」為 /Models2目錄下的 UserTable2s
            // SQL Profile翻譯後的成果
            // exec sp_executesql N'SELECT [u].[UserId] AS [uID], [u].[UserName] AS [uNAME], [u].[UserSex] AS [uSEX], [u].[UserBirthDay] AS [uBIRTHDAY], [u].[DepartmentId] AS [uDEPARTMENTID], [d].[DepartmentId], [d].[DepartmentName], [d].[DepartmentYear]
            //            FROM[UserTable2] AS[u]
            // LEFT JOIN[DepartmentTable2] AS[d] ON[u].[DepartmentId] = [d].[DepartmentId]
            // WHERE[u].[DepartmentId] = @___ID_0',N'@___ID_0 int',@___ID_0=3
        }

        //***********************************************
        // 以 UserTable2s（學生）為主角。  需搭配 ViewModel。  透過 UserTable2s類別的「導覽屬性」來串連資料
        // 輸出的「匿名類型」，記得使用 ViewModel承接 ( /Models2目錄下的 UserTable2sViewModel ) 並展示在檢視畫面上
        public IActionResult IdexMasterDetails2_NavigationVM(int _ID = 1)// 給 _ID變數（科系ID），一個預設值
        {
            var result = from ut in _db.UserTable2
                         where ut.DepartmentId == _ID
                         select new UserTable2ViewModel
                         {
                             //*** 重點！！如果您不使用 ViewModel，直接使用 UserTable2類別//    ************************
                             // 執行檢視畫面時，就會出錯 --「LINQ to Entities 查詢中無法建構實體或複雜類型 'WebApplication2017_MVC_GuestBook.Models2.UserTable2'。」
                             UserId = ut.UserId,
                             UserName = ut.UserName,
                             UserSex = ut.UserSex,
                             UserBirthDay = ut.UserBirthDay,
                             UserMobilePhone = ut.UserMobilePhone,
                             UserApproved = ut.UserApproved,
                             DepartmentTable2 = ut.DepartmentTable2  //透過 UserTable2s類別的「導覽屬性」來串連資料

                         };
           


            return View(result);
        }
        public IActionResult IdexMasterDetails2_NavigationVMTest(int _ID = 1)// 給 _ID變數（科系ID），一個預設值
        {
            var result = from ut in _db.UserTable2
                         where ut.DepartmentId == _ID
                         select new UserTable2
                         {
                             //*** 重點！！如果您不使用 ViewModel，直接使用 UserTable2類別//    ************************
                             // 執行檢視畫面時，就會出錯 --「LINQ to Entities 查詢中無法建構實體或複雜類型 'WebApplication2017_MVC_GuestBook.Models2.UserTable2'。」
                             UserId = ut.UserId,
                             UserName = ut.UserName,
                             UserSex = ut.UserSex,
                             UserBirthDay = ut.UserBirthDay,
                             UserMobilePhone = ut.UserMobilePhone,
                             UserApproved = ut.UserApproved,
                             DepartmentTable2 = ut.DepartmentTable2  //透過 UserTable2s類別的「導覽屬性」來串連資料

                         };



            return View(result);
        }
        //=====================================
        // (2). ViewModel版（檔名 /Models2/UserDepartmentViewModel.cs）。 
        //       兩個 Table做 JOIN 並且展示在畫面上。
        // 缺點：重複的資料（科系）會不斷出現。需要額外過濾。
        //=====================================

        // 使用 ViewModel（一對一），檔名 UserDepartmentViewModel.cs
        // 同上一個範例。透過 Inner JOIN 查詢語法
        public IActionResult IdexMasterDetails5()
        {
            var result = from d in _db.DepartmentTable2
                         join u in _db.UserTable2 on d.DepartmentId equals u.DepartmentId
                         select new UserDepartmentViewModel { DepartmentVM = d, UserVM = u };

            return View(result);
        }


        public IActionResult IdexMasterDetails5_Fix()
        {
            var result = from d in _db.DepartmentTable2
                         join u in _db.UserTable2 on d.DepartmentId equals u.DepartmentId
                         select new UserDepartmentViewModel { DepartmentVM = d, UserVM = u };

            return View(result.ToList());
        }


        // 同上一個 IndexMasterDetails5 範例。加入 group by (將查詢結果分組)
        // 上一個範例（IndexMasterDetails5）有個嚴重的錯誤，在檢視畫面上，「科系」會重複出現
        // (1) ***必須改用ViewModel*** 位於 Models2 目錄下的 UDViewModel.cs檔 （一對多）
        // (2) 改用 .GroupJopin()方法來做
        public IActionResult IndexMasterDetails5_GroupBy()
        {
            // .GroupJoin()方法   https://dotnettutorials.net/lesson/linq-group-join/
            var GroupJoinMS = _db.DepartmentTable2.ToList()//Outer Data Source （科系）
                                                                 //***************** .NET Core不加上這一句就會報錯，無法轉換格式！
                                                 .GroupJoin(_db.UserTable2,//Inner Data Source（學生）
                                                 dept => dept.DepartmentId,//Outer Key Selector  i.e. the Common Property
                                                 stu => stu.DepartmentId,//Inner Key Selector  i.e. the Common Property
                                                 (dept, stu) => new { dept, stu });//Projecting the Result to an Anonymous Type

            // (2) 搭配 ViewModel，輸出檢視畫面
            // 位於 Models2 目錄下的 UDViewModel.cs檔 （一對多）
            List<UDViewModel> myList = new List<UDViewModel>();
            foreach (var item in GroupJoinMS)
            {
                UDViewModel userUDViewModel = new UDViewModel();
                userUDViewModel.DVM = item.dept; // (1) 科系。加入UDViewModel類別 (ViewModel)

                // (2) 這個科系底下的所有「學生」。Inner Foreach loop for each employee of a Particular department
                List<UserTable2>myStuList = new List<UserTable2>();  //這個科系底下的所有「學生」
                foreach (UserTable2 student in item.stu)
                {
                    myStuList.Add(student);  //單一學生，加入 List裡面
                }
                userUDViewModel.UVM = myStuList; //把這個科系的「所有學生List<UserTable2>」加入UDViewModel類別 (ViewModel)裡面
                // 學生。加入UDViewModel類別 (ViewModel)
                myList.Add(userUDViewModel);
            }
            return View(myList);
        }
    }
}
