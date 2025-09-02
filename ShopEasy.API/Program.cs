using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopEasy.API.Data;
using ShopEasy.API.Extensions;
using ShopEasy.API.Services;

var builder = WebApplication.CreateBuilder(args);


var keyVaultEndpoint = builder.Configuration["AzureKeyVault:Endpoint"];
if (!string.IsNullOrEmpty(keyVaultEndpoint))
{
    // Use DefaultAzureCredential which works locally (with az login) and on Azure (with Managed Identity)
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
}


//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(80); // Container will listen on port 80
//});


// 1.  Custom Services 
builder.Services.AddAppService();

// 2. Application DB Services - Now this will get the actual connection string from Key Vault or local config
var connectionString = builder.Configuration["DbConnection"] ??
                      builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Identity Services
builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddEntityFrameworkStores<AppDbContext>().
    AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Lockout.MaxFailedAccessAttempts = 3; });

//4. Authorization and Authentication
builder.Services.AddAuthorization();

//5. Automapper & Utilities
builder.Services.AddAutoMapper(typeof(Program));

// 6. Controllers & Swaggers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthentication();

//Customer middleware after Authentication...
app.UseAccessTokenValidatorMiddleWare();
app.UseGuestMiddleWare();

//app.UseMiddleware()

//Before authorization we can do customer customer Route matching 

app.UseAuthorization();

//Route Matching..
app.MapControllers();

app.Run();
