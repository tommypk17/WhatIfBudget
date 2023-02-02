var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSpaStaticFiles(c =>
{
    c.RootPath = "ClientApp/dist";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
});


app.Run();
