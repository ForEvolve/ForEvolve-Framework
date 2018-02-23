using Microsoft.Extensions.Logging;
using System;

namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class UserIdFinderTraceDecorator : IUserIdFinder
    {
        private readonly ILogger _logger;
        private readonly IUserIdFinder _decorated;
        public UserIdFinderTraceDecorator(IUserIdFinder decorated, ILogger<UserIdFinderTraceDecorator> logger)
        {
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GetUserId()
        {
            var userId = _decorated.GetUserId();
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
