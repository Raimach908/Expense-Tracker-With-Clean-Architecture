using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Application.UseCases.ExpenseUseCases;
using ExpenseTracker.Application.UseCases.UserUseCases;
using ExpenseTracker.Core.Entities;
using System;

namespace MyExpenseTrackerWithCookies.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly GetAllExpensesUseCase _getAllExpensesUseCase;
        private readonly CreateExpenseUseCase _createExpenseUseCase;
        private readonly GetExpenseByIdUseCase _getExpenseByIdUseCase;
        private readonly UpdateExpenseUseCase _updateExpenseUseCase;
        private readonly DeleteExpenseUseCase _deleteExpenseUseCase;
        private readonly GetTotalExpenseUseCase _getTotalExpenseUseCase;
        private readonly GetLastExpenseUseCase _getLastExpenseUseCase;
        private readonly GetMonthlyReportUseCase _getMonthlyReportUseCase;
        private readonly GetYearlyReportUseCase _getYearlyReportUseCase;
        private readonly GetDailyReportUseCase _getDailyReportUseCase;
        private readonly GetUserByEmailUseCase _getUserByEmailUseCase;

        public ExpenseController(
            GetAllExpensesUseCase getAllExpensesUseCase,
            CreateExpenseUseCase createExpenseUseCase,
            GetExpenseByIdUseCase getExpenseByIdUseCase,
            UpdateExpenseUseCase updateExpenseUseCase,
            DeleteExpenseUseCase deleteExpenseUseCase,
            GetTotalExpenseUseCase getTotalExpenseUseCase,
            GetLastExpenseUseCase getLastExpenseUseCase,
            GetMonthlyReportUseCase getMonthlyReportUseCase,
            GetYearlyReportUseCase getYearlyReportUseCase,
            GetDailyReportUseCase getDailyReportUseCase,
            GetUserByEmailUseCase getUserByEmailUseCase)
        {
            _getAllExpensesUseCase = getAllExpensesUseCase;
            _createExpenseUseCase = createExpenseUseCase;
            _getExpenseByIdUseCase = getExpenseByIdUseCase;
            _updateExpenseUseCase = updateExpenseUseCase;
            _deleteExpenseUseCase = deleteExpenseUseCase;
            _getTotalExpenseUseCase = getTotalExpenseUseCase;
            _getLastExpenseUseCase = getLastExpenseUseCase;
            _getMonthlyReportUseCase = getMonthlyReportUseCase;
            _getYearlyReportUseCase = getYearlyReportUseCase;
            _getDailyReportUseCase = getDailyReportUseCase;
            _getUserByEmailUseCase = getUserByEmailUseCase;
        }

        public IActionResult Index(string search)
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            var expenses = _getAllExpensesUseCase.Execute(user.UserId);

            if (!string.IsNullOrEmpty(search))
            {
                expenses = expenses.Where(i => i.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(expenses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ExpenseEntity());
        }

        [HttpPost]
        public IActionResult Create(ExpenseEntity expense)
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            expense.UserId = user.UserId;

            try
            {
                _createExpenseUseCase.Execute(expense);
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(expense);
            }
        }

        public IActionResult Edit(int id)
        {
            var expense = _getExpenseByIdUseCase.Execute(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        [HttpPost]
        public IActionResult Edit(ExpenseEntity expense)
        {
            if (ModelState.IsValid)
            {
                _updateExpenseUseCase.Execute(expense);
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        public IActionResult Delete(int id)
        {
            var expense = _getExpenseByIdUseCase.Execute(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _deleteExpenseUseCase.Execute(id);
            return RedirectToAction("Index");
        }

        public IActionResult TotalExpense()
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            var totalExpense = _getTotalExpenseUseCase.Execute(user.UserId);

            ViewBag.TotalExpense = totalExpense;
            return View();
        }

        public IActionResult LastExpense()
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            var lastExpense = _getLastExpenseUseCase.Execute(user.UserId);

            ViewBag.LastExpense = lastExpense;
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
            var monthlyExpense = _getMonthlyReportUseCase.Execute(user.UserId, date);

            ViewBag.MonthlyExpense = monthlyExpense;
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
            var yearlyExpense = _getYearlyReportUseCase.Execute(user.UserId, year);

            ViewBag.YearlyExpense = yearlyExpense;
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
            var dailyExpense = _getDailyReportUseCase.Execute(user.UserId, date);

            ViewBag.DailyExpense = dailyExpense;
            return View();
        }
    }
}
