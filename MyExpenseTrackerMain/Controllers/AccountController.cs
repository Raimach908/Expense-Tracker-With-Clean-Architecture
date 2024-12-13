using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Application.UseCases.UserUseCases;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Application.Model_Binding;
using System;

namespace MyExpenseTrackerWithCookies.Controllers
{
    public class AccountController : Controller
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;
        private readonly UpdateUserProfileUseCase _updateUserProfileUseCase;
        private readonly DeleteUserProfileUseCase _deleteUserProfileUseCase;
        private readonly GetUserByEmailUseCase _getUserByEmailUseCase;
        public readonly GetUserProfileUseCase _getUserProfileUseCase;
        public readonly CheckUserExistsUseCase _checkUserExistsUseCase;

        public AccountController(
            RegisterUserUseCase registerUserUseCase,
            LoginUserUseCase loginUserUseCase,
            UpdateUserProfileUseCase updateUserProfileUseCase,
            DeleteUserProfileUseCase deleteUserProfileUseCase,
            GetUserByEmailUseCase getUserByEmailUseCase,
            GetUserProfileUseCase getUserProfileUseCase,
            CheckUserExistsUseCase checkUserExistsUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _loginUserUseCase = loginUserUseCase;
            _updateUserProfileUseCase = updateUserProfileUseCase;
            _deleteUserProfileUseCase = deleteUserProfileUseCase;
            _getUserByEmailUseCase = getUserByEmailUseCase;
            _getUserProfileUseCase = getUserProfileUseCase;
            _checkUserExistsUseCase = checkUserExistsUseCase;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if ( _checkUserExistsUseCase.Execute(model.Email) != false)
                {
                    
                    ViewBag.ErrorMessage = "Email already exists.";
                    return View(model);
                }

                var user = new UserEntity
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNo = model.PhoneNo,
                    CreatedDate = DateTime.Now
                };

                _registerUserUseCase.Execute(user);
                TempData["Message"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _loginUserUseCase.Execute(model.Email, model.Password);

                    if (user == null)
                    {
                        // Redirect to Register if the user is not found
                        TempData["ErrorMessage"] = "This email is not registered. Please create an account.";
                        return RedirectToAction("Register");
                    }

                    // Set cookie for logged-in user
                    var options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(2)
                    };
                    Response.Cookies.Append("AuthCookie", user.Email, options);

                    TempData["Message"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }
                catch (UnauthorizedAccessException)
                {
                    ViewBag.ErrorMessage = "Invalid email or password.";
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthCookie");
            TempData["Message"] = "You have been logged out.";
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            var email = Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult UpdateProfile(int userId)
        {
            var user = _getUserProfileUseCase.Execute(userId); 
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UserEntity user)
        {
            if (ModelState.IsValid)
            {
                // Fetch the existing user data
                var existingUser = _getUserProfileUseCase.Execute(user.UserId);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Retain the existing profile picture if it's not updated
                if (user.ProfilePicturePath == null || user.ProfilePicturePath.Length == 0)
                {
                    user.ProfilePicturePath = existingUser.ProfilePicturePath;
                }

                // Update the user profile
                _updateUserProfileUseCase.Execute(user);
                TempData["Message"] = "Profile updated successfully!";
                return RedirectToAction("Profile");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult DeleteProfile(int userId)
        {
            var user = _getUserProfileUseCase.Execute(userId); // Assuming a method to fetch user by ID
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(UserEntity user)
        {
            if (user != null)
            {
                _deleteUserProfileUseCase.Execute(user.UserId);
                Response.Cookies.Delete("AuthCookie");
                TempData["Message"] = "User profile has been successfully deleted.";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "Error occurred while deleting the profile.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult UploadProfilePicture(IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    profilePicture.CopyTo(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    var email = Request.Cookies["AuthCookie"];
                    if (string.IsNullOrEmpty(email))
                    {
                        return Json(new { success = false, message = "User not logged in." });
                    }

                    var user = _getUserByEmailUseCase.Execute(email);
                    if (user == null)
                    {
                        return Json(new { success = false, message = "User not found." });
                    }

                    user.ProfilePicturePath = fileBytes;
                    _updateUserProfileUseCase.Execute(user);

                    var base64Image = Convert.ToBase64String(fileBytes);
                    return Json(new { success = true, newProfilePicturePath = base64Image });
                }
            }

            return Json(new { success = false, message = "No file selected." });
        }

        [HttpPost]
        public IActionResult DeleteProfilePicture()
        {
            var email = Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "User not logged in." });
            }

            var user = _getUserByEmailUseCase.Execute(email);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            user.ProfilePicturePath = new byte[0];
            _updateUserProfileUseCase.Execute(user);

            return Json(new { success = true, message = "Profile picture deleted successfully." });
        }

        [HttpGet]
        public IActionResult ViewProfileImage()
        {
            var email = Request.Cookies["AuthCookie"];
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login");
            }

            var user = _getUserByEmailUseCase.Execute(email);
            if (user?.ProfilePicturePath == null || user.ProfilePicturePath.Length == 0)
            {
                // Fallback to default image
                var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "defaultProfilePic.jpg");
                var defaultImageData = System.IO.File.ReadAllBytes(defaultImagePath);
                return File(defaultImageData, "image/jpeg");
            }

            return File(user.ProfilePicturePath, "image/png");
        }

    }
}
