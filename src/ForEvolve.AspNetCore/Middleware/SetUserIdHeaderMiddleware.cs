using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ForEvolve.AspNetCore.Middleware
{
    public class SetUserIdHeaderMiddleware : BaseMiddleware
    {
        private readonly IUserIdAccessor _userIdAccessor;
        private readonly UserIdFinderSettings _userIdFinderSettings;

        public SetUserIdHeaderMiddleware(RequestDelegate next, IUserIdAccessor userIdAccessor, UserIdFinderSettings userIdFinderSettings)
            : base(next)
        {
            _userIdAccessor = userIdAccessor ?? throw new ArgumentNullException(nameof(userIdAccessor));
            _userIdFinderSettings = userIdFinderSettings ?? throw new ArgumentNullException(nameof(userIdFinderSettings));
        }

        protected override Task InternalInvokeAsync(HttpContext context)
        {
            if (_userIdAccessor.HasUserId())
            {
                if (!HasUserIdHeader(context))
                {
                    string userId = _userIdAccessor.FindUserId();
                    context.Request.Headers.TryAdd(_userIdFinderSettings.HeaderName, userId);
                }
            }
            return Task.CompletedTask;
        }

        private bool HasUserIdHeader(HttpContext context)
        {
            return context.Request.Headers.ContainsKey(_userIdFinderSettings.HeaderName);
        }
    }

    public interface IUserIdAccessor
    {
        string FindUserId();
        bool HasUserId();
    }

    public class DelegatedUserIdAccessor : IUserIdAccessor
    {
        private readonly Func<string> _findUserIdDelegate;
        private readonly Func<bool> _hasUserIdDelegate;

        public DelegatedUserIdAccessor(Func<string> findUserIdDelegate, Func<bool> hasUserIdDelegate)
        {
            _findUserIdDelegate = findUserIdDelegate ?? throw new ArgumentNullException(nameof(findUserIdDelegate));
            _hasUserIdDelegate = hasUserIdDelegate ?? throw new ArgumentNullException(nameof(hasUserIdDelegate));
        }

        public string FindUserId()
        {
            return _findUserIdDelegate();
        }

        public bool HasUserId()
        {
            return _hasUserIdDelegate();
        }
    }
}
