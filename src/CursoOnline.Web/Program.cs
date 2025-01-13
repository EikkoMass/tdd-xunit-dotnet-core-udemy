using CursoOnline.Dominio._Base;
using CursoOnline.Ioc;
using CursoOnline.Web;

var builder = WebApplication.CreateBuilder(args);

StartupIoc.BuildServices(builder.Services, builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Use(async (context, next) => //Middleware para sempre commitar
{
    await next.Invoke();
    
    var unitOfWork = (IUnitOfWork) context.RequestServices.GetService(typeof(IUnitOfWork));
    await unitOfWork.Commit();
});

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