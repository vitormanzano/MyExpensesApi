using System.Security.Claims;

namespace MyExpenses.UserContext
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public Guid UserId
        {
            get
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    throw new UnauthorizedAccessException("User ID not found in token!");

                return Guid.Parse(userIdClaim);
            }
        }
    }
}
