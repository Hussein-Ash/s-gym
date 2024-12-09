using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EvaluationBackend.Entities;

namespace API.Extensions;

public static class ClaimsPrincipleExtensions
{
  
    public static Guid GetUserId(this ClaimsPrincipal user) => 
        Guid.Parse(GetClaim(user,"Id") ?? throw new Exception("Cannot get id from token"));
    
    public static string GetUserFullName(this ClaimsPrincipal user) =>
        GetClaim(user,"FullName") ?? throw new Exception("Cannot get FullName from token");
    
    public static UserRole GetUserRole(this ClaimsPrincipal user) => 
        Enum.Parse<UserRole>(GetClaim(user,"Role"));
    public static DateTime GetTokenExpier(this ClaimsPrincipal user) =>    
        DateTime.Parse(GetClaim(user,"ExpierDate"));

    public static string GetParentId(this ClaimsPrincipal user) => GetClaim(user,"ParentId");  




     private static string GetClaim(this ClaimsPrincipal user ,string claimName)
    {
        var claims = (user.Identity as ClaimsIdentity)?.Claims;
        var claim = claims?.FirstOrDefault(c =>
            string.Equals(c.Type, claimName, StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(c.Value, "null", StringComparison.CurrentCultureIgnoreCase));

        return claim?.Value?.Replace("\"", "") ?? string.Empty;
 
    }
}