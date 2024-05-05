using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Services;
using System.Text;
using System.Net;
using Azure.Messaging.ServiceBus;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllers();
services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SE 4458 Midterm", Version = "v1" });
    c.SwaggerDoc("1-Midterm Controller", new OpenApiInfo { Title = "1-Midterm Controller", Version = "v1" });
    c.SwaggerDoc("2-Generator Controller", new OpenApiInfo { Title = "2-Generator Controller", Version = "v1" });
    c.SwaggerDoc("3-Generic Controller", new OpenApiInfo { Title = "3-Generic Controller", Version = "v1" });
    c.SwaggerDoc("4-Example Controller", new OpenApiInfo { Title = "4-Example Controller", Version = "v1" });
    // Optionally, add XML comments
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

string serviceBusConnectionString = "Endpoint=sb://unituitionpaymentsrvbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=zJgGPu5LZbEeqvDuQ6nShsEcMIIdYSXiy+ASbG9syus=";
string queueName = "unituitionpaymentqueue";
services.AddSingleton<ServiceBusClient>(provider =>
{
    return new ServiceBusClient(serviceBusConnectionString);
});

string connectionString = "Data Source=KaansAsusWorkPC\\SQLEXPRESS;Initial Catalog=UniversityTuitionPaymentV2;Integrated Security=True;Trust Server Certificate=True";
services.AddDbContext<UniversityTuitionPaymentContext>(options => { options.UseSqlServer(connectionString); });

services.AddScoped<IBankAccountService, BankAccountService>();
services.AddScoped<IBankAccountTransferService, BankAccountTransferService>();
services.AddScoped<IStudentService, StudentService>();
services.AddScoped<ITermService, TermService>();
services.AddScoped<ITuitionService, TuitionService>();
services.AddScoped<IUniversityService, UniversityService>();
services.AddScoped<IMessageQueueService>(provider =>
{
    return new MessageQueueService(serviceBusConnectionString, queueName);
});;

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/1-Midterm Controller/swagger.json", "SE 4458 Midterm Controllers");
    c.SwaggerEndpoint("/swagger/2-Generator Controller/swagger.json", "Generator Controller");
    c.SwaggerEndpoint("/swagger/3-Generic Controller/swagger.json", "Generic Controllers");
    c.SwaggerEndpoint("/swagger/4-Example Controller/swagger.json", "Example Controller");
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger - V1");
});
/*app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/1-Midterm Controller/swagger.json", "SE 4458 Midterm Controllers");
    c.SwaggerEndpoint("/swagger/2-Generator Controller/swagger.json", "Generator Controller");
    c.SwaggerEndpoint("/swagger/3-Generic Controller/swagger.json", "Generic Controllers");
    c.SwaggerEndpoint("/swagger/4-Example Controller/swagger.json", "Example Controller");
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger - V1");
});*/


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




