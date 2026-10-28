using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Infrastructures;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContextMusicPortal(connection);
builder.Services.AddMusicPortalProfile();
builder.Services.AddUnitOfWork();
builder.Services.AddMusicService();
builder.Services.AddGenreService();
builder.Services.AddUserService();
builder.Services.AddPasswordHashing();
builder.Services.AddReadLanguage();
builder.Services.AddSession();

var app = builder.Build();

app.UseSession();
app.UseStaticFiles();

app.MapGet("/Download/{fileName}", (string fileName) =>
{
    var pathAudio = Path.Combine(builder.Environment.WebRootPath, "Music", fileName);
    return Results.File(pathAudio, "audio/ogg", fileName);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Music}/{action=Index}/{id?}");


app.Run();
