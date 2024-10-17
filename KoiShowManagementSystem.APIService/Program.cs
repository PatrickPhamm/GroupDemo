using KoiShowManagementSystem.Service;
using KoiShowManagementSystem.Service.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:7184")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});


builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<ResultService>();
builder.Services.AddScoped<IJudgeService, JudgeService>();
builder.Services.AddScoped<JudgeService>();
builder.Services.AddScoped<IJudgesCriteriaService, JudgesCriteriaService>();
builder.Services.AddScoped<IContestService, ContestService>();
builder.Services.AddScoped<IKoiBusiness, KoiService>();
builder.Services.AddScoped<IApplicationBusiness, ApplicationService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
