//using WebApi;

//List<Person> users = new List<Person>()
//{
//    new Person(){Id=1,Name="Vasya",Age=24},
//    new Person(){Id=2,Name="Boris",Age=16},
//    new Person(){Id=3,Name="Artem",Age=22}
//};

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();
//app.UseDefaultFiles();
//app.UseStaticFiles();
//app.MapGet("/api/users", () => users);
//app.MapGet("/api/user/{id}", (int id) =>
//    {
//        Person? user = users.FirstOrDefault(u => u.Id == id);
//        if (user == null) return Results.NotFound(new {message="Пользователь не найден"});
//        return Results.Json(user);
//    }       
//);
//app.MapDelete("/api/user/{id}", (int id) =>
//    {
//        Person? user = users.FirstOrDefault(u => u.Id == id);
//        if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
//        users.Remove(user);
//        return Results.Json(user);
//    }
//);
//app.MapPost("/api/user", (Person person) =>
//{
//    users.Add(person);
//    return person;
//});
//app.MapPut("/api/user", (Person person) =>
//{
//    var user=users.FirstOrDefault(u => u.Id == person.Id);
//    if(user==null) return Results.NotFound(new { message = "Пользователь не найден" });
//    user.Name = person.Name;
//    user.Age = person.Age;
//    return Results.Json(user);
//});
//app.Run();
using Microsoft.EntityFrameworkCore;
using WebApi;
var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection = builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app=builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGet("/api/users",async (ModelDB db)=>await db.Users.ToListAsync());
app.MapGet("/api/users/{id:int}", async (int id, ModelDB db) =>
{
    Person? person = await db.Users.FirstOrDefaultAsync(u=> u.Id == id);
    if (person == null) return Results.NotFound(new { message = "Пользователь не найден" });
    return Results.Json(person);
});
app.MapDelete("/api/users/{id:int}", async (int id, ModelDB db) =>
{
    Person? person = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
    if (person == null) return Results.NotFound(new { message = "Пользователь не найден" });
    db.Users.Remove(person);
    await db.SaveChangesAsync();
    return Results.Json(person);
});
app.MapPost("/api/users", async (Person person, ModelDB db) =>
{
    await db.Users.AddAsync(person);
    await db.SaveChangesAsync();
    return person;
});
app.MapPut("/api/users", async (Person p, ModelDB db) =>
{
    Person? person = await db.Users.FirstOrDefaultAsync(u => u.Id == p.Id);
    if (person == null) return Results.NotFound(new { message = "Пользователь не найден" });
    person.Age = p.Age;
    person.Name = p.Name;
    await db.SaveChangesAsync();
    return Results.Json(person);
});
app.Run();
