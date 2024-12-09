using System.Globalization;
using API.SignalR;
using EvaluationBackend;
using EvaluationBackend.DATA;
using EvaluationBackend.Extensions;
using EvaluationBackend.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using ConfigurationProvider = EvaluationBackend.Helpers.ConfigurationProvider;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
.MinimumLevel.Error()
.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            // .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000" ,"https://dashboard-tau-fawn.vercel.app/")
            .AllowCredentials()
    );
});

// Add services to the container.

builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    options.SerializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal });
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}); ;


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<PascalCaseQueryParameterFilter>();
});
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSignalR();

IConfiguration configuration = builder.Configuration;
ConfigurationProvider.Configuration = configuration;
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(
    c =>
{
    c.DocExpansion(DocExpansion.None);

    //styling swagger
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "structure v1");
    // c.SwaggerEndpoint("/swagger/v2/swagger.json", "structure v2");
    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    
}
);




app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");
app.UseMiddleware<CustomUnauthorizedMiddleware>();
app.UseMiddleware<CustomPayloadTooLargeMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseWebSockets();





app.MapControllers();
app.MapHub<MessageHub>("chathub/message");


app.Run();