using Lab3;
using Lab3.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection = builder.Services.AddDbContext<Lab3Isp32Context>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login", async (User loginData, Lab3Isp32Context db) =>
{
    User? person = await db.Users!.FirstOrDefaultAsync(p => p.Email == loginData.Email &&
p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.Email!) };
    var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.Now.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = person.Email
    };
    return Results.Json(response);
});

app.MapGet("/api/Pricelist", [Authorize] async (Lab3Isp32Context db) => await db.PriceLists!.ToListAsync());
app.MapGet("api/Pricelist/{id:int}", [Authorize] async (Lab3Isp32Context db, int id) => await db.PriceLists!.Where(g => g.Id == id).FirstOrDefaultAsync());
app.MapGet("/api/Product", [Authorize] async (Lab3Isp32Context db) => await db.products!.ToListAsync());
app.MapGet("/api/Pricelist/{name}", [Authorize] async (Lab3Isp32Context db, string name) => await db.PriceLists!.Where(u => u.Name == name).FirstOrDefaultAsync());

app.MapPost("/api/Product", [Authorize] async (Product product, Lab3Isp32Context db) =>
{
    await db.products!.AddAsync(product);
    await db.SaveChangesAsync();
    return product;
});
app.MapPost("/api/PriceList", [Authorize] async (PriceList PriceList, Lab3Isp32Context db) =>
{
    await db.PriceLists!.AddAsync(PriceList);
    await db.SaveChangesAsync();
    return PriceList;
});
app.MapDelete("/api/PriceList/{id:int}", [Authorize] async (int id, Lab3Isp32Context db) =>
{
    PriceList? PriceList = await db.PriceLists!.FirstOrDefaultAsync(u => u.Id == id);
    if (PriceList == null) return Results.NotFound(new { message = "прайслист не найден" });
    db.PriceLists!.Remove(PriceList);
    await db.SaveChangesAsync();
    return Results.Json(PriceList);
});
app.MapPut("/api/PriceList", [Authorize] async (PriceList PriceList, Lab3Isp32Context db) =>
{
    PriceList? g = await db.PriceLists!.FirstOrDefaultAsync(u => u.Id == PriceList.Id);
    if (g == null) return Results.NotFound(new { message = "прайслист не найден" });
    g.Name = PriceList.Name;
    g.Coast = PriceList.Coast;
    g.Id = PriceList.Id;
    await db.SaveChangesAsync();
    return Results.Json(g);
});
app.MapPut("/api/Product", [Authorize] async (Product product, Lab3Isp32Context db) =>
{
    Product? st = await db.products!.FirstOrDefaultAsync(u => u.Id == product.Id);
    if (st == null) return Results.NotFound(new { message = "продукт не найдена" });
    st.SaleDate = product.SaleDate;
    st.Quantity = product.Quantity;
    st.ProductCoast = product.ProductCoast;

    await db.SaveChangesAsync();
    return Results.Json(st);
});
app.Run();
