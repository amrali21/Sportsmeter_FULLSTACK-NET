using CRUD_Design;
using CRUD_Design.Contracts;
using CRUD_Design.Repository;
using CRUD_Design_Contracts;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sportsmeter_frontend.Model.Services;
using System.Net;

// will you see my commit?
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connstring));

builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CRUD_DESIGN")
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRunInfoRepository, RunInfoRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:61405", "http://127.0.0.1:5500")
               .AllowCredentials()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddRepoServices();
builder.Services.AddAuthServices(builder);

var app = builder.Build();

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
            response.StatusCode == (int)HttpStatusCode.Forbidden)
        response.Redirect("/Home/Error");
});

#region copied


CookiePolicyOptions cookiePolicyOptions = new()
{
    HttpOnly = HttpOnlyPolicy.Always
};

cookiePolicyOptions.Secure = CookieSecurePolicy.SameAsRequest;
cookiePolicyOptions.MinimumSameSitePolicy = SameSiteMode.Unspecified; // same site, what does it mean?

app.UseCookiePolicy(cookiePolicyOptions);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseAuthentication();
app.UseAuthorization();

#endregion

app.Run();
