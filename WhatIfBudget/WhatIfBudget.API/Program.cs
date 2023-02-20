using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Logic;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services;
using WhatIfBudget.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext"));
});

//Services
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IBudgetIncomeService, BudgetIncomeService>();
builder.Services.AddScoped<IBudgetExpenseService, BudgetExpenseService>();

//Logic
builder.Services.AddScoped<IExpenseLogic, ExpenseLogic>();
builder.Services.AddScoped<IIncomeLogic, IncomeLogic>();
builder.Services.AddScoped<IBudgetLogic, BudgetLogic>();

builder.Services.AddControllers();

//Setup MS AD B2C Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                {
                    builder.Configuration.Bind("AzureAd", options);

                    options.TokenValidationParameters.NameClaimType = "name";
                },
                    options => { builder.Configuration.Bind("AzureAd", options); });

builder.Services.AddSwaggerGen();

//Setup App CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp",
        policy =>
            policy.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(builder.Configuration.GetSection("CORS:allowed").Get<string[]>())
    );
});

//Build the App
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowWebApp");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
