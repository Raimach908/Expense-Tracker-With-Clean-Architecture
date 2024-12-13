using ExpenseTracker.Application.UseCases.BudgetUseCases;
using ExpenseTracker.Application.UseCases.ContactUseCases;
using ExpenseTracker.Application.UseCases.ExpenseUseCases;
using ExpenseTracker.Application.UseCases.IncomeUseCases;
using ExpenseTracker.Application.UseCases.UserUseCases;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Get the connection string from the configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Check if the connection string is null or empty
 // Register repositories with dependency injection
// Method 1
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Use Cases
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<GetUserByEmailUseCase>();
builder.Services.AddScoped<UpdateUserProfileUseCase>();
builder.Services.AddScoped<DeleteUserProfileUseCase>();
builder.Services.AddScoped<GetUserProfileUseCase>();
builder.Services.AddScoped<CheckUserExistsUseCase>();

builder.Services.AddScoped<IIncomeRepository>(provider => new IncomeRepository(connectionString));
// Use Cases
builder.Services.AddScoped<GetAllIncomeUseCase>();
builder.Services.AddScoped<CreateIncomeUseCase>();
builder.Services.AddScoped<GetIncomeByIdUseCase>();
builder.Services.AddScoped<UpdateIncomeUseCase>();
builder.Services.AddScoped<DeleteIncomeUseCase>();
builder.Services.AddScoped<GetUserByEmailUseCase>();
builder.Services.AddScoped<GetTotalIncomeUseCase>();
builder.Services.AddScoped<GetLastIncomeUseCase>();
builder.Services.AddScoped<ExpenseTracker.Application.UseCases.IncomeUseCases.GetMonthlyReportUseCase>();
builder.Services.AddScoped<ExpenseTracker.Application.UseCases.IncomeUseCases.GetYearlyReportUseCase>();
builder.Services.AddScoped<ExpenseTracker.Application.UseCases.IncomeUseCases.GetDailyReportUseCase>();
builder.Services.AddScoped<GetUserByEmailUseCase>();

builder.Services.AddScoped<IExpenseRepository>(provider => new ExpenseRepository(connectionString, provider.GetRequiredService<IIncomeRepository>()));
builder.Services.AddScoped<GetAllExpensesUseCase>();
builder.Services.AddScoped<CreateExpenseUseCase>();
builder.Services.AddScoped<GetExpenseByIdUseCase>();
builder.Services.AddScoped<UpdateExpenseUseCase>();
builder.Services.AddScoped<DeleteExpenseUseCase>();
builder.Services.AddScoped<GetUserByEmailUseCase>();
builder.Services.AddScoped<GetTotalExpenseUseCase>();
builder.Services.AddScoped<GetLastExpenseUseCase>();
builder.Services.AddScoped<ExpenseTracker.Application.UseCases.ExpenseUseCases.GetMonthlyReportUseCase>();
builder.Services.AddScoped<ExpenseTracker.Application.UseCases.ExpenseUseCases.GetYearlyReportUseCase>();
builder.Services.AddScoped<ExpenseTracker.Application.UseCases.ExpenseUseCases.GetDailyReportUseCase>();

builder.Services.AddScoped<IBudgetRepository>(provider => new BudgetRepository(connectionString));
// Budget Use Cases
builder.Services.AddScoped<GetAllBudgetsUseCase>();
builder.Services.AddScoped<GetBudgetByIdUseCase>();
builder.Services.AddScoped<AddBudgetUseCase>();
builder.Services.AddScoped<UpdateBudgetUseCase>();
builder.Services.AddScoped<DeleteBudgetUseCase>();
builder.Services.AddScoped<IContactRepository>(provider => new ContactRepository(connectionString));
// Contact Use Cases
builder.Services.AddScoped<AddContactUseCase>();
builder.Services.AddScoped<GetAllContactsUseCase>();

// Method 2 (I dont use this method bcz I passed connection strings in constructors)
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
//builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
//builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
//builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
