using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Model.Requests
{
    public class SignupRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

    }
}
