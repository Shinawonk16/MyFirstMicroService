using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.SetBasePath(context.HostingEnvironment.ContentRootPath)
        .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
});

builder.Services.AddControllers();
builder.Services.AddOcelot();
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
app.UseRouting();

// app.MapControllers();
app.UseOcelot().Wait();
  app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

app.Run();
