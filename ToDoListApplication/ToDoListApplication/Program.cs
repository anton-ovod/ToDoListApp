using GraphiQl;
using GraphQL;
using GraphQL.Types;
using ToDoListApplication.Factories.Implementations.Repository;
using ToDoListApplication.Factories.Implementations.StorageContext;
using ToDoListApplication.Factories.Infrastructure;
using ToDoListApplication.GraphQL.Queries;
using ToDoListApplication.GraphQL.Schema;
using ToDoListApplication.GraphQL.Types;
using ToDoListApplication.Middlewares;
using ToDoListApplication.Providers;
using ToDoListApplication.StorageContext.Implementations.DbStorageContext;
using ToDoListApplication.StorageContext.Implementations.FileStorageContext;
using ToDoListApplication.StorageContext.Infrastructure;
using ToDoListApplication.Strategy;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:7053")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


builder.Services.AddSingleton<StorageTypeProvider>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DapperSQLContext>();
builder.Services.AddScoped<XMLStorageContext>();


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

app.UseMiddleware<StorageTypeMiddleware>();

app.UseRouting();

app.UseCors("CorsPolicy"); // Enable CORS


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseGraphiQl("/graphql");
app.UseGraphQL<ISchema>();
app.Run();
