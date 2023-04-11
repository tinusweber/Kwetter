using AuthService.Model.Requests;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(SignupRequest request)
        {
            this.Email = request.Email;
            this.UserName = request.Username;
        }
        public Guid? StoreToManage { get; set; }
    }
}
