using Microsoft.Extensions.Logging;
using System;

namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class UserIdAccessorTraceDecorator : IUserIdAccessor
    {
        private readonly ILogger _logger;
        private readonly IUserIdAccessor _decorated;
        public UserIdAccessorTraceDecorator(IUserIdAccessor decorated, ILogger<UserIdAccessorTraceDecorator> logger)
        {
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string FindUserId()
        {
            var userId = _decorated.FindUserId();
            _logger.LogTrace($"UserId: {userId}");
            return userId;
        }

        public bool HasUserId()
        {
            var hasUserId = _decorated.HasUserId();
            _logger.LogTrace($"HasUserId: {hasUserId}");
            return hasUserId;
        }
    }
}
