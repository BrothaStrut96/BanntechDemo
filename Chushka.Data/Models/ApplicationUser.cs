using System;
using Microsoft.AspNetCore.Identity;

namespace Chushka.Data.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
