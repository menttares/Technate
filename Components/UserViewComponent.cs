using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Technate.Components;
[ViewComponent]
public class UserViewComponent : ViewComponent
{
    public string Invoke()
    {
        if (User.Identity.IsAuthenticated)
        {
            string username = ((ClaimsIdentity)User.Identity).FindFirst("username")?.Value;
            return username;
        }

        return "Гость";
    }
}

