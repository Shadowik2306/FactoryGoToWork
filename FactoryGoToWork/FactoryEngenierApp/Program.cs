using FactoryBusinessLogic.BusinessLogics;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.StoragesContracts;
using FactoryDatabaseImplement.Implements;
using FactoryEngenierApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=IndexComponent}/{id?}");

app.Run();
