using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Application.UseCases.BudgetUseCases;
using ExpenseTracker.Application.UseCases.UserUseCases;
using ExpenseTracker.Core.Entities;
using System;

namespace MyExpenseTrackerWithCookies.Controllers
{
    public class BudgetController : Controller
    {
        private readonly GetAllBudgetsUseCase _getAllBudgetsUseCase;
        private readonly GetBudgetByIdUseCase _getBudgetByIdUseCase;
        private readonly AddBudgetUseCase _addBudgetUseCase;
        private readonly UpdateBudgetUseCase _updateBudgetUseCase;
        private readonly DeleteBudgetUseCase _deleteBudgetUseCase;
        private readonly GetUserByEmailUseCase _getUserByEmailUseCase;

        public BudgetController(
            GetAllBudgetsUseCase getAllBudgetsUseCase,
            GetBudgetByIdUseCase getBudgetByIdUseCase,
            AddBudgetUseCase addBudgetUseCase,
            UpdateBudgetUseCase updateBudgetUseCase,
            DeleteBudgetUseCase deleteBudgetUseCase,
            GetUserByEmailUseCase getUserByEmailUseCase)
        {
            _getAllBudgetsUseCase = getAllBudgetsUseCase;
            _getBudgetByIdUseCase = getBudgetByIdUseCase;
            _addBudgetUseCase = addBudgetUseCase;
            _updateBudgetUseCase = updateBudgetUseCase;
            _deleteBudgetUseCase = deleteBudgetUseCase;
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
            var budgets = _getAllBudgetsUseCase.Execute(user.UserId);

            if (!string.IsNullOrEmpty(search))
            {
                budgets = budgets.Where(b => b.category.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(budgets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new BudgetEntity());
        }

        [HttpPost]
        public IActionResult Create(BudgetEntity budget)
        {
            var email = HttpContext.Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            budget.UserId = user.UserId;

            _addBudgetUseCase.Execute(budget);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var budget = _getBudgetByIdUseCase.Execute(id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        [HttpPost]
        public IActionResult Edit(BudgetEntity budget)
        {
            if (ModelState.IsValid)
            {
                _updateBudgetUseCase.Execute(budget);
                return RedirectToAction("Index");
            }

            return View(budget);
        }

        public IActionResult Delete(int id)
        {
            var budget = _getBudgetByIdUseCase.Execute(id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _deleteBudgetUseCase.Execute(id);
            return RedirectToAction("Index");
        }
    }
}
