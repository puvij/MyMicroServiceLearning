using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models
{
    public class ApplicaionUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
