using Microsoft.AspNetCore.Mvc;
using YetiMunch.Models;
using YetiMunch.Services.Interfaces;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserDTO loginRequest)
    {
        var result = await _authService.Register(loginRequest);
        if (!result)
        {
            ModelState.AddModelError("Username", "Username or email already taken.");
            return View(loginRequest);
        }
        return View("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserDTO requestedUser)
    {
        var (token, hotels, user) = await _authService.Login(requestedUser);
        if (token == null)
        {
            ModelState.AddModelError("Password", "Invalid username or password.");
            return View(requestedUser);
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,   
            SameSite = SameSiteMode.Strict, 
            Expires = DateTime.UtcNow.AddHours(1) 
        };
        Response.Cookies.Append("JWTToken", token, cookieOptions);

        if (user.Username == "admin" && user.Email == "admin@admin")
        {
            return View("Dashboard");
        }

        return View("Marketplace",hotels);
    }

    [HttpPost]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("JWTToken"); 
        return View("Login");
    }
}
