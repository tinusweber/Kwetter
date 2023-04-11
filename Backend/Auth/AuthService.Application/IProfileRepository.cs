using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application
{
    public interface IProfileRepository
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context);
        public Task IsActiveAsync(IsActiveContext context);

    }
}
