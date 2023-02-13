using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Common.Models
{
    public class AuthUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public static AuthUser Current(IPrincipal principal)
        {
            var res = new AuthUser();
            try
            {
                ClaimsPrincipal claimsPrincipal = (ClaimsPrincipal)principal;
                var claims = claimsPrincipal.Claims.ToList();
                
                var uidClaim = claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier");
                var firstNameClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName);
                var lastNameClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname);
                var emailClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                
                var uid = Guid.Empty.ToString();
                if (uidClaim is not null) uid = uidClaim.Value;
                res.Id = Guid.Parse(uid);

                var firstName = string.Empty;
                if(firstNameClaim is not null) firstName = firstNameClaim.Value;
                res.FirstName = firstName;

                var lastName = string.Empty;
                if (lastNameClaim is not null) lastName = lastNameClaim.Value;
                res.LastName = lastName;

                var email = string.Empty;
                if (emailClaim is not null) email = emailClaim.Value;
                res.Email = email;

                return res;
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("Could not cast IPrincipal to ClaimsPrincipal");
            }
        }
    }
}
