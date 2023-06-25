using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;     // ConfigurationBuilder�|�Ψ�o�өR�W�Ŷ�
using Microsoft.Extensions.DependencyInjection;
using WebApplication2022_Core6_6th_RelationalDB;
using WebApplication2022_Core6_6th_RelationalDB.Models2;   // ���d�ҩ�b /Models2�ؿ��U




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//**** Ū�� appsettings.json �]�w�ɸ̭�����ơ]��Ʈw�s���r��^****
////�@�k�@�G
//builder.Services.AddDbContext<MVC_UserDB2Context>(options => options.UseSqlServer("Server=.\\sqlexpress;Database=MVC_UserDB2;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=False;;MultipleActiveResultSets=true"));
//// �o�̻ݭn�s�W��өR�W�Ŷ��A�ШϥΡu��ܥi�઺�ץ��v���t�Φۤv�[�W�C
//// System.ComponentModel.Win32Exception (0x80090325): �������쵲�O�Ѥ����H�������v���o�X���Chttps://devmanna.blogspot.com/2019/02/aspnet-mvc-sqlexception.html

////�@�k�G�G Ū���]�w�ɪ����e
//// ��ƨӷ�  �{���X  https://github.com/CuriousDrive/EFCore.AllDatabasesConsidered/blob/main/MS%20SQL%20Server/Northwind.MSSQL/Program.cs
builder.Services.AddDbContext<MVC_UserDB2Context>(
        options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.EnableDetailedErrors(true);
        });


////�@�k�T�G Ū���]�w�ɪ����e
//var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
//IConfiguration config = configurationBuilder.Build();   // ConfigurationBuilder�|�Ψ� Microsoft.Extensions.Configuration�R�W�Ŷ�
//string DBconnectionString = config["ConnectionStrings:DefaultConnection"];  // appsettings.josn�ɸ̭����u��Ʈw�s���r��v
//builder.Services.AddDbContext<MVC_UserDB2Context>(options => options.UseSqlServer(DBconnectionString));

//======================
//�ϥΧ����A�|"�۰�"������Ʈw�s�u�C https://docs.microsoft.com/zh-tw/aspnet/core/data/ef-mvc/crud?view=aspnetcore-5.0
//======================
// �z�|�I�s .AddDbContext() �X�R��k �Ӧb ASP.NET Core DI �e�����G�� DbContext ���O�C
// �ھڹw�]�A�Ӥ�k�|�N�A�Ȧs�d���]�w�� Scoped�C
// Scoped ��ܤ��e���󪺦s�d���|�P Web �n�D���s�d���O���@�P�A�åB�b Web �n�D�����ɷ|�۰ʩI�s Dispose ��k�C
//********************************************************************



//=== �� �j �u ===============================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
