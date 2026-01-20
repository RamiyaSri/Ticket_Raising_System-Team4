using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using TicketClassLibrary.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITicketPriorityRepository, EFTicketPriorityRepository>();
builder.Services.AddScoped<ITicketRepository, EFTicketRepository>();
builder.Services.AddScoped<ITicketTypeRepository, EFTicketTypeRepository>();
builder.Services.AddScoped<IEmployeeRepository, EFEmployeeRepository>();
builder.Services.AddScoped<ITicketAssignmentRepository, EFTicketAssignmentRepository>();
builder.Services.AddScoped<ITicketCommentRepository, EFTicketCommentRepository>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authentication using Bearer scheme"
    });
 
    options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", doc),
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters =
        new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://www.team4.com",
            ValidAudience = "https://www.team4.com",
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                       "My name is not important but my key is very important!"
                    )
                )
        };
});
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
 
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
 
app.Run();