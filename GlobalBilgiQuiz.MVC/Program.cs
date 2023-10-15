using GlobalBilgiQuiz.Business;
using GlobalBilgiQuiz.Business.Repositories;
using GlobalBilgiQuiz.Business.Services.AdminServiceFolder;
using GlobalBilgiQuiz.Business.Services.QuizServiceFolder;
using GlobalBilgiQuiz.Business.SignalRHubs;
using GlobalBilgiQuiz.Business.UnitOfWorkFolder;
using GlobalBilgiQuiz.Data.POCO;
using GlobalBilgiQuiz.Database.DbContexts;
using GlobalBilgiQuiz.MVC.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllersWithViews().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

services.AddDbContext<GlobalBilgiQuizDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:Dev").Value);
}, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

services.AddApplicationServices(builder.Configuration);
services.AddScoped<IQuizService, QuizService>();
services.AddScoped<IAdminService, AdminService>();
services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddScoped<QuestionPageFilter>();
services.AddScoped<EndContestFilter>();
services.AddScoped<AdminFilter>();

services.AddSignalR();

services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(120);
});
services.AddCors(o =>
{
    o.AddPolicy("All", p =>
    {
        p.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseCors("All");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<QuizHub>("/quizhub");
    //endpoints.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();


// {controller=Home}/{action=Index}/{id?}