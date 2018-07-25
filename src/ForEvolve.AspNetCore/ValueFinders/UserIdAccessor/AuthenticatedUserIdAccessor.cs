﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class AuthenticatedUserIdAccessor : IUserIdAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticatedUserIdAccessorSettings _options;

        public AuthenticatedUserIdAccessor(IHttpContextAccessor httpContextAccessor, AuthenticatedUserIdAccessorSettings options)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public string FindUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userID = user.FindFirst(_options.UserIdClaimType);
            return userID.Value;
        }

        public bool HasUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var hasUserIdClaim = user.HasClaim(c => c.Type == _options.UserIdClaimType);
            return hasUserIdClaim;
        }
    }
}
