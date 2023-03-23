using Microsoft.AspNetCore.Identity;
using authService.DomainModels.Requests;

namespace authService.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(SingupRequest request)
        {
            this.Email = request.Email;
            this.UserName = request.Username;
        }
        public Guid? StoreToManage { get; set; }
    }
}
