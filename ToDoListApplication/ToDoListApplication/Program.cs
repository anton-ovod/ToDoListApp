using GraphiQl;
using GraphQL;
using GraphQL.Types;
using ToDoListApplication.Factories.Implementations.Repository;
using ToDoListApplication.Factories.Implementations.StorageContext;
using ToDoListApplication.Factories.Infrastructure;
using ToDoListApplication.GraphQL.Queries;
using ToDoListApplication.GraphQL.Schema;
using ToDoListApplication.GraphQL.Types;
using ToDoListApplication.StorageContext.Infrastructure;
using ToDoListApplication.Strategy;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IStorageContextFactory, StorageContextFactory>();
builder.Services.AddScoped(provider => 
        provider.GetRequiredService<IStorageContextFactory>().GetStorageContext());

builder.Services.AddScoped<IStrategyFactory, StrategyFactory>();

builder.Services.AddScoped(provider => 
        provider.GetRequiredService<IStrategyFactory>().CreateRepositoryStrategy());

builder.Services.AddScoped(provider => 
        provider.GetRequiredService<IRepositoryStrategy>().CreateTaskRepository());

builder.Services.AddScoped(provider =>
        provider.GetRequiredService<IRepositoryStrategy>().CreateCategoryRepository());

builder.Services.AddScoped(provider =>
        provider.GetRequiredService<IRepositoryStrategy>().CreateTaskStatusRepository());

builder.Services.AddScoped<TaskQuery>();

builder.Services.AddGraphQL(b => b
    .AddSystemTextJson()
    .AddGraphTypes(typeof(RootSchema).Assembly)
    .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = builder.Environment.IsDevelopment())
);

builder.Services.AddTransient<ISchema, RootSchema>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseGraphiQl("/graphql");
app.UseGraphQL<ISchema>();
app.Run();
