using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationWorkshop.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            const string Issuer = "https://contoso.com";
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "Lil' Wayne", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim(ClaimTypes.Role, "TalentedRapper", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim(ClaimTypes.Role, "BadSkateboarder", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim("EmployeeId", "247", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim(ClaimTypes.DateOfBirth, "1982-09-27", ClaimValueTypes.Date));
            claims.Add(new Claim("BadgeNumber", "amilli", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim("TemporaryBadgeExpiry",
                     DateTime.Now.AddDays(-1000).ToString(),
                     ClaimValueTypes.String,
                     Issuer));
            var userIdentity = new ClaimsIdentity("SuperSecureLogin");
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });

            return RedirectToLocal(returnUrl);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
