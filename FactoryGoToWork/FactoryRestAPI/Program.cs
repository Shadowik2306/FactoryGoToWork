using FactoryBusinessLogic.BusinessLogics;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.StoragesContracts;
using FactoryDatabaseImplement.Implements;
using Microsoft.OpenApi.Models;
using PrecastConcretePlantDatabaseImplement.Implements;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.


builder.Services.AddTransient<IComponentStorage, ComponentStorage>();
builder.Services.AddTransient<IEngenierStorage, EngenierStorage>();
builder.Services.AddTransient<ILatheBusyStorage, LatheBusyStorage>();
builder.Services.AddTransient<ILatheStorage, LatheStorage>();
builder.Services.AddTransient<IMasterStorage, MasterStorage>();
builder.Services.AddTransient<IPlanStorage, PlanStorage>();
builder.Services.AddTransient<IReinforcedStorage, ReinforcedStorage>();
builder.Services.AddTransient<IStageStorage, StageStorage>();

builder.Services.AddTransient<IComponentLogic, ComponentLogic>();
builder.Services.AddTransient<IEngenierLogic, EngenierLogic>();
builder.Services.AddTransient<ILatheBusyLogic, LatheBusyLogic>();
builder.Services.AddTransient<ILatheLogic, LatheLogic>();
builder.Services.AddTransient<IMasterLogic, MasterLogic>();
builder.Services.AddTransient<IPlanLogic, PlanLogic>();
builder.Services.AddTransient<IReinforcedLogic, ReinforcedLogic>();
builder.Services.AddTransient<IStageLogic, StageLogic>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "FactoryRestAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();

	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FactoryContracts v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();