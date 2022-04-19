using Microsoft.AspNetCore.Mvc;
using Ships.API.Middlewares;
using Ships.API.Validations;
using Ships.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Ships.API.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Ships.DTO.xml"));
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors(options =>
    options.AddPolicy(name: "allowAll",
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyMethod()
                .AllowAnyOrigin();
        }));

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.ReportApiVersions = true;
});

builder.Services.RegisterValidators();
builder.Services.RegisterApplication();

var app = builder.Build();

app.UseMiddleware<ErrorResponseMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ships API"));
app.UseSwagger();
app.UseRouting();
app.UseCors("allowAll");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
