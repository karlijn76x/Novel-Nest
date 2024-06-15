using Novel_Nest_DAL;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using Novel_Nest_Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.AddUserSecrets<Program>();
var configuration = builder.Configuration;

string ConnectionString = configuration.GetConnectionString("ConnectionString");
builder.Services.AddScoped<UserService>();


builder.Services.AddSingleton<IBookRepository>(new BookRepository(ConnectionString));
builder.Services.AddSingleton<ICategoryRepository>(new CategoryRepository(ConnectionString));
builder.Services.AddSingleton<IUserDB>(new UserDB(ConnectionString));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<APIService>();

builder.Services.AddHttpClient();

// Configure session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // example timeout value
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseHttpMethodOverride();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Enable session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
