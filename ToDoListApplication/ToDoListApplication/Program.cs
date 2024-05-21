using ToDoListApplication.Factory;
using ToDoListApplication.Models.Data;
using ToDoListApplication.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DapperDBContext>();
builder.Services.AddSingleton<XMLStorageContext>();
builder.Services.AddSingleton<RepositoryFactory>();
builder.Services.AddTransient<ITaskRepository, XMLTaskRepository>();
builder.Services.AddTransient<ICategoryRepository, XMLCategoryRepository>();
builder.Services.AddTransient<ITaskStatusRepository, XMLTaskStatusRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
