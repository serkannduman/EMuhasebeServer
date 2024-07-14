using EMuhasebeServer.Application.Features.Auth.Login;
using EMuhasebeServer.Domain.Entities;

namespace EMuhasebeServer.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
