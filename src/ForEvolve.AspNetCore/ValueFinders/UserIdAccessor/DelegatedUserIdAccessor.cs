using System;

namespace ForEvolve.AspNetCore
{
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
