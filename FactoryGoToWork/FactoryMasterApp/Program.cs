
using FactoryBusinessLogic.BusinessLogics;
using FactoryBusinessLogic.OfficePackage.Implements;
using FactoryBusinessLogic.OfficePackage;
using FactoryContracts.BusinessLogicsContracts;
using FactoryContracts.StoragesContracts;
using FactoryDatabaseImplement.Implements;
using FactoryMasterApp;
using FactoryBusinessLogic.Mail;
using FactoryContracts.BindingModels;

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

builder.Services.AddTransient<IReportLogic, ReportLogic>();
builder.Services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
builder.Services.AddTransient<AbstractSaveToWord, SaveToWord>();
builder.Services.AddTransient<AbstractSaveToPdf, SaveToPdf>();

builder.Services.AddSingleton<MailWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var mailSender = app.Services.GetService<MailWorker>();
mailSender?.MailConfig(new MailConfigBindingModel
{
    MailLogin = builder.Configuration?.GetSection("MailLogin")?.Value?.ToString() ?? string.Empty,
    MailPassword = builder.Configuration?.GetSection("MailPassword")?.Value?.ToString() ?? string.Empty,
    SmtpClientHost = builder.Configuration?.GetSection("SmtpClientHost")?.Value?.ToString() ?? string.Empty,
    SmtpClientPort = Convert.ToInt32(builder.Configuration?.GetSection("SmtpClientPort")?.Value?.ToString()),
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=IndexLathe}/{id?}");

app.Run();
