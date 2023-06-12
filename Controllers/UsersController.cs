using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CompanyManager.Services;


/*
it is not required to explicitly define the [HttpPost] attribute for the CreateUser action method. By convention, 
if you have a method named CreateUser within a controller and it accepts an HTTP POST request, 
ASP.NET Core will automatically recognize it as a POST action.
*/


[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly IEmailService _emailManager;

    public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailService emailManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailManager = emailManager;

    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new IdentityUser
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            TwoFactorEnabled = model.TwoFactorEnabled,
            // Add additional properties as needed
        };

        await _userManager.SetTwoFactorEnabledAsync(user, true);


            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(result.Errors);
            }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return BadRequest("Invalid username or password");
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        if (result.Succeeded)
        {
            return Ok();
        }
        else
        {
            return BadRequest("Invalid username or password");
        }
    }
    [HttpPost("recover")]
    public async Task<IActionResult> RecoverPassword([FromBody] LoginDto model)
    {

        await _emailManager.SendEmailAsync(model.UserName, "Recover Password", "Your password is: " + model.Password);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if(model.UserName == null)
        {
            return BadRequest("Invalid username or password");
        }

        return Ok();
    }

    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }
        // Add additional properties as needed
    }

    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
