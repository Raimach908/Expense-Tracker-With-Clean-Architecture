using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Application.UseCases.IncomeUseCases;
using ExpenseTracker.Application.UseCases.UserUseCases;
using ExpenseTracker.Core.Entities;

namespace MyExpenseTrackerWithCookies.Controllers
{
    public class IncomeController : Controller
    {
        private readonly GetAllIncomeUseCase _getAllIncomeUseCase;
        private readonly CreateIncomeUseCase _createIncomeUseCase;
        private readonly GetIncomeByIdUseCase _getIncomeByIdUseCase;
        private readonly UpdateIncomeUseCase _updateIncomeUseCase;
        private readonly DeleteIncomeUseCase _deleteIncomeUseCase;
        private readonly GetUserByEmailUseCase _getUserByEmailUseCase;
        private readonly GetTotalIncomeUseCase _getTotalIncomeUseCase;
        private readonly GetLastIncomeUseCase _getLastIncomeUseCase;
        private readonly GetMonthlyReportUseCase _getMonthlyReportUseCase;
        private readonly GetYearlyReportUseCase _getYearlyReportUseCase;
        private readonly GetDailyReportUseCase _getDailyReportUseCase;

        public IncomeController(
            GetAllIncomeUseCase getAllIncomeUseCase,
            CreateIncomeUseCase createIncomeUseCase,
            GetIncomeByIdUseCase getIncomeByIdUseCase,
            UpdateIncomeUseCase updateIncomeUseCase,
            DeleteIncomeUseCase deleteIncomeUseCase,
            GetUserByEmailUseCase getUserByEmailUseCase,
            GetTotalIncomeUseCase getTotalIncomeUseCase,
            GetLastIncomeUseCase getLastIncomeUseCase,
            GetMonthlyReportUseCase getMonthlyReportUseCase,
            GetYearlyReportUseCase getYearlyReportUseCase,
            GetDailyReportUseCase getDailyReportUseCase)
        {
            _getAllIncomeUseCase = getAllIncomeUseCase;
            _createIncomeUseCase = createIncomeUseCase;
            _getIncomeByIdUseCase = getIncomeByIdUseCase;
            _updateIncomeUseCase = updateIncomeUseCase;
            _deleteIncomeUseCase = deleteIncomeUseCase;
            _getUserByEmailUseCase = getUserByEmailUseCase;
            _getTotalIncomeUseCase = getTotalIncomeUseCase;
            _getLastIncomeUseCase = getLastIncomeUseCase;
            _getMonthlyReportUseCase = getMonthlyReportUseCase;
            _getYearlyReportUseCase = getYearlyReportUseCase;
            _getDailyReportUseCase = getDailyReportUseCase;
        }

        public IActionResult Index(string search)
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var incomes = _getAllIncomeUseCase.Execute(user.UserId);

            if (!string.IsNullOrEmpty(search))
            {
                incomes = incomes.Where(i => i.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(incomes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IncomeEntity income)
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            income.UserId = user.UserId;
            _createIncomeUseCase.Execute(income);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var income = _getIncomeByIdUseCase.Execute(id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        [HttpPost]
        public IActionResult Edit(IncomeEntity income)
        {
            if (ModelState.IsValid)
            {
                _updateIncomeUseCase.Execute(income);
                return RedirectToAction("Index");
            }

            return View(income);
        }

        public IActionResult Delete(int id)
        {
            var income = _getIncomeByIdUseCase.Execute(id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _deleteIncomeUseCase.Execute(id);
            return RedirectToAction("Index");
        }

        public IActionResult TotalIncome()
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            var totalIncome = _getTotalIncomeUseCase.Execute(user.UserId);

            ViewBag.TotalIncome = totalIncome;
            return View();
        }

        public IActionResult LastIncome()
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            var lastIncome = _getLastIncomeUseCase.Execute(user.UserId);

            ViewBag.LastIncome = lastIncome;
            return View();
        }

        public IActionResult MonthlyReport(DateTime date)
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            var monthlyIncome = _getMonthlyReportUseCase.Execute(user.UserId, date);

            ViewBag.MonthlyIncome = monthlyIncome;
            return View();
        }

        public IActionResult YearlyReport(int year)
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            var yearlyIncome = _getYearlyReportUseCase.Execute(user.UserId, year);

            ViewBag.YearlyIncome = yearlyIncome;
            return View();
        }

        public IActionResult DailyReport(DateTime date)
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            var dailyIncome = _getDailyReportUseCase.Execute(user.UserId, date);

            ViewBag.DailyIncome = dailyIncome;
            return View();
        }
    }
}
