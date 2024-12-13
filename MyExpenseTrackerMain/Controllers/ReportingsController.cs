using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.web.Controllers
{
    public class ReportingsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IIncomeRepository _incomeRepository;

        public ReportingsController(IUserRepository userRepository, IExpenseRepository expenseRepository, IIncomeRepository incomeRepository)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
            _incomeRepository = incomeRepository;
        }
        //Index_
        public IActionResult Index()
        {
            var user = GetLoggedInUserId();
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); 
            }
            int userId = user.UserId;

            // Fetch total income, expense, balance, and last income/expense
            var totalIncome = _incomeRepository.GetTotalIncome(userId);
            var totalExpense = _expenseRepository.GetTotalExpense(userId);
            var lastIncome = _incomeRepository.GetLastIncome(userId);
            var lastExpense = _expenseRepository.GetLastExpense(userId);

            // Fetch reports
            var monthlyReport = _incomeRepository.GetMonthlyReport(userId, DateTime.Now); 
            var yearlyReport = _incomeRepository.GetYearlyReport(userId, DateTime.Now.Year); 
            var dailyReport = _incomeRepository.GetDailyReport(userId, DateTime.Today);

            // Pass data to the view
            var model = new ReportingsEntity
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                LastIncome = lastIncome,
                LastExpense = lastExpense,
                MonthlyReport = monthlyReport,
                YearlyReport = yearlyReport,
                TotalBalance = totalIncome - totalExpense,
                DailyReport = dailyReport
            };
            
            return View(model);
        }

        private UserEntity GetLoggedInUserId()
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];

            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return user;
        }
    }
}
