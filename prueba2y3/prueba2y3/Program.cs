using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Enable Kestrel on all IPs (so Nginx can connect)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5020);
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.UseForwardedHeaders();

// Enable Swagger (optional for dev/testing)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();