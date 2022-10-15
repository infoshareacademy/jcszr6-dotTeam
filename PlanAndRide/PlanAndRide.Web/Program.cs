using EmailService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using PlanAndRide.Database.Repository;
using IEmailSender = EmailService.IEmailSender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.       
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();



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

builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.SignIn.RequireConfirmedEmail = true;
});


var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<PlanAndRideContext>(
    options =>
    {
        options.UseSqlServer(connectionString);
        options.LogTo(Console.WriteLine, LogLevel.Information);
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);
var emailConfig = config.GetSection("EmailConfiguration")
  .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();
var app = builder.Build();


// Configure the HTTP request pipeline.>
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<PlanAndRideContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedRolesAsync(userManager, roleManager);
        await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
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
