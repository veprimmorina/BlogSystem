using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlogSystem.Core.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using BlogSystem.Infrastructure.Repositories;
using BlogSystem.Core.Validators;
using BlogSystem.Core.DTO;
using BlogSystem.Core.Interfaces.Repositories;
using BlogSystem.Core.Interfaces.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Define user roles
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


var key = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // You can set this to true if you want to validate the issuer
        ValidateAudience = false, // You can set this to true if you want to validate the audience
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
    };
});

// Add services to the container.
builder.Services.AddScoped<IBlogService, BlogPostService>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IValidator<BlogPostDto>, BlogPostDtoValidator>();

builder.Services.AddMvc()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
