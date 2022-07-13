using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DBContext.Context;
using Domain.POCOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models.UserRequestServiceModels;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly TravelDbContext _context; 
    private readonly string _username;
    private readonly IHttpContextAccessor _httpContext;
    
    public UserService(IJwtService jwtService, UserManager<ApplicationUser> userManager, 
    IApplicationUserRepository applicationUserRepository, TokenValidationParameters tokenValidationParameters, TravelDbContext context, IHttpContextAccessor httpContext)
    {
        _userManager = userManager;
        _applicationUserRepository = applicationUserRepository;
        _tokenValidationParameters = tokenValidationParameters;
        _context = context;
        _httpContext = httpContext;
        _jwtService = jwtService;
        _username = _httpContext.HttpContext.User.Identity.Name;
    }

    public async Task<ApplicationUser> GetInfoAsync()
    {
        var user = await _applicationUserRepository.GetFullInfoAsync(_username);
        return user;
    }

    public async Task<(string, string)> AuthenticationAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            throw new InvalidCredentialsException(ExceptionMessages.InvalidCredentials);
        var check = await _userManager.CheckPasswordAsync(user, password);
        if (!check)
            throw new InvalidCredentialsException(ExceptionMessages.InvalidCredentials);
        return await _jwtService.GenerateSecurityTokenAsync(user.Id, username);
    }

    public async Task<IEnumerable<IdentityError>> CreateAsync(CreateUserServiceModel user)
    {
        var entityByName = await _userManager.FindByNameAsync(user.UserName);
        var entityByEmail = await _userManager.FindByEmailAsync(user.Email);
        
        if (entityByName != null)
            throw new ObjectAlreadyExistsException(ExceptionMessages.UsernameAlreadyExists);
        if (entityByEmail != null)
            throw new ObjectAlreadyExistsException(ExceptionMessages.EmailAlreadyExists);
        
        var applicationUser = new ApplicationUser()
        {
            UserName = user.UserName,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Image = Convert.FromBase64String(user.Image),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(applicationUser, user.Password);
        if (result != null)
            return result.Errors;
        await _userManager.AddToRoleAsync(applicationUser, "Basic");
        return null;
    }

    public async Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByNameAsync(username);
        var check = await _userManager.CheckPasswordAsync(user, oldPassword);
        if (check is false)
            throw new InvalidCredentialsException(ExceptionMessages.InvalidCredentials);
        await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return true;
    }

    public async Task UpdateUserInfoAsync(UpdateUserInfoServiceModel request)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        user.UserName = request.UserName;
        user.Firstname = request.Firstname;
        user.Lastname = request.Lastname;
        user.Image = Convert.FromBase64String(request.Image);
        user.Bio = request.Bio;
        
        await _userManager.UpdateAsync(user);
    }

    public async Task<(string, string)> RefreshTokenAsync(string requestToken, string requestRefreshToken)
    {
        var validatedToken = GetPrincipalFromToken(requestToken);

        if (validatedToken == null)
            return new ValueTuple<string, string>("invalid token", null);

        var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == requestRefreshToken);

        if (storedRefreshToken == null)
            return new ValueTuple<string, string>("This Refresh token does not exist", null);

        if (DateTime.UtcNow > storedRefreshToken.ExpireDate)
            return new ValueTuple<string, string>("this refresh token has expired", null);
        
        if (storedRefreshToken.Invalidated)
            return new ValueTuple<string, string>("this refresh token has invalidated", null);
        
        if(storedRefreshToken.Used)
            return new ValueTuple<string, string>("this refresh token has been used", null);

        if(storedRefreshToken.JwtId != jti)
            return new ValueTuple<string, string>("this refresh token does not match this JWT", null);

        storedRefreshToken.Used = true;
        _context.RefreshTokens.Update(storedRefreshToken);

        await _context.SaveChangesAsync();

        var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
        return await _jwtService.GenerateSecurityTokenAsync(user.Id, user.UserName);
    }

    private ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            if(!IsJwtWithValidSecurityAlgorithm(validatedToken))
                return null;
            return principal;
        }
        catch
        {
            return null;
        }
    }

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}