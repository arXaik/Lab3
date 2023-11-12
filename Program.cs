using Lab3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
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
builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login", async (User loginData,ModelDB db) =>
{
    User? person =await db.Users.FirstOrDefaultAsync(p => p.Email == loginData.Email && p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email!) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encodedJwt,
        username = person.Email
    };

    return Results.Json(response);
});
app.MapGet("api/rate", [Authorize] async (ModelDB db) => await db.Rate.ToListAsync());
app.MapGet("api/rate/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Rate? rate = await db.Rate.FirstOrDefaultAsync(x => x.Id == id);
    if (rate == null) return Results.NotFound(new { message = "Rate not found" });
    return Results.Json(rate);
});
app.MapGet("api/cassette", [Authorize] async (ModelDB db) => await db.Cassette.ToListAsync());
app.MapGet("api/cassette/select/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Cassette cassette = await db.Cassette.FirstOrDefaultAsync(x => x.Id == id);
    if (cassette == null) return Results.NotFound(new { message = "Cassette not found" });
    return Results.Json(cassette);
});
app.MapGet("api/cassette/select/{data}", [Authorize] async (string data, ModelDB db) =>
{
    List<Cassette> cassette = await db.Cassette.Where(x => x.DateCassette == DateTime.Parse(data)).ToListAsync();
    return cassette;
});
app.MapPost("/api/cassette", [Authorize] async (Cassette cas, ModelDB db) =>
{
    await db.Cassette.AddAsync(cas);
    await db.SaveChangesAsync();
    return cas;
});
app.MapPost("/api/rate", [Authorize] async (Rate rate, ModelDB db) =>
{
    await db.Rate.AddAsync(rate);
    await db.SaveChangesAsync();
    return rate;
});
app.MapDelete("api/cas/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Cassette? cas = await db.Cassette.FirstOrDefaultAsync(x => x.Id == id);
    if (cas == null) return Results.NotFound(new { message = "Cas not found" });
    db.Cassette.Remove(cas);
    await db.SaveChangesAsync();
    return Results.Json(cas);
});
app.MapDelete("api/rete/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Rate? rate = await db.Rate.FirstOrDefaultAsync(x => x.Id == id);
    if (rate == null) return Results.NotFound(new { message = "User not found" });
    db.Rate.Remove(rate);
    await db.SaveChangesAsync();
    return Results.Json(rate);
});
app.MapPut("/api/cas", [Authorize] async (Cassette casData, ModelDB db) =>
{
    Cassette? cas = await db.Cassette.FirstOrDefaultAsync(u => u.Id == casData.Id);
    if (cas == null) return Results.NotFound(new { message = "Cas not found" });
    cas.Type = casData.Type;
    cas.Genre = casData.Genre;
    cas.Film = casData.Film;
    cas.DateCassette = casData.DateCassette;
    cas.FIO = casData.FIO;
    cas.Term = casData.Term;
    await db.SaveChangesAsync();
    return Results.Json(cas);
});
app.MapPut("/api/pricelist", [Authorize] async (Rate rateData, ModelDB db) =>
{
    Rate? rate = await db.Rate.FirstOrDefaultAsync(u => u.Id == rateData.Id);
    if (rate == null) return Results.NotFound(new { message = "Rate not found" });
    rate.Type = rateData.Type;
    rate.Genre = rateData.Genre;
    await db.SaveChangesAsync();
    return Results.Json(rate);
});
app.Run();