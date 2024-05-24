using Novel_Nest_DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
//new BookRepository(new MyDbContext("Server=127.0.0.1;Database=Novel_Nest_Db;Uid=root;"))
//builder.Services.AddScoped(IBoo)
// Configure session state
builder.Services.AddSession(options =>
{
    // Configure session options if needed
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

app.UseAuthorization();

// Enable session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
