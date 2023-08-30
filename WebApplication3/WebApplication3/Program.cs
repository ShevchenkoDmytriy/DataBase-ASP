using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebApplication3;

var builder = WebApplication.CreateBuilder(args);


string connection = "Data Source =HOME-PC; Initial Catalog = adonetdb; Trusted_Connection=True";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));


var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", async (ApplicationContext db) => await db.Users.ToListAsync());


app.MapGet("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{

    User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    if (user == null)
        return Results.NotFound(new { message = "Пользователь не найден" });


    return Results.Json(user);

});

app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{

    User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    if (user == null)
        return Results.NotFound(new { message = "Пользователь не найден" });

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(user);

});


app.MapPost("/api/users/{id:int}", async (User user, ApplicationContext db) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return user;

});

app.MapPut("/api/users", async (User userData, ApplicationContext db) =>
{

    User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

    if (user == null)
        return Results.NotFound(new { message = "Пользователь не найден" });


    user.Name = userData.Name;
    user.Price = userData.Price;
    await db.SaveChangesAsync();
    return Results.Json(user);

});

app.Run();