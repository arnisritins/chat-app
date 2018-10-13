using Microsoft.AspNetCore.Identity;

namespace ChatApp.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public virtual string FullName { get; set; }
    }
}
