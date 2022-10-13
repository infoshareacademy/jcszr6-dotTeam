using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using PlanAndRide.Database.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IRideRepository, RideRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IRepository<Club>, ClubRepository>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IRideService, RideService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IClubService, ClubService>();

//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<PlanAndRideContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PlanAndRideContext>() 
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication()
        .AddGoogle(google =>
        {
            google.ClientId = "937689180086-vrtb4tbfqghme5st02676jqjdj5s3e8g.apps.googleusercontent.com";
            google.ClientSecret = "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";
            google.SignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddFacebook(facebookOptions =>
         {
             facebookOptions.AppId = "459357132843873";
             facebookOptions.AppSecret = "4dc94b33644d387662bee62bcef6960f";
             facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
         })
        .AddTwitter(twitterOptions =>
        {
            twitterOptions.ConsumerKey = "QUl4U05VaWQxQWlYd2ZXLTg3eEE6MTpjaQ";
            twitterOptions.ConsumerSecret = "JiezdUb7FeDk7pePrDFW83HH3IkRvvM0O8bMOTJTCl1C6OfoEU";
        })
        .AddLinkedIn(linkedInOptions =>
        {
            linkedInOptions.ClientId = "77yc8nr6j8531g";
            linkedInOptions.ClientSecret = "HV93P3HOIKaCZc66";
        });

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<PlanAndRideContext>(
    options =>
    {
        options.UseSqlServer(connectionString);
        options.LogTo(Console.WriteLine, LogLevel.Information);
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();


// Configure the HTTP request pipeline.>
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseRequestLocalization("en-US");

app.Run();
