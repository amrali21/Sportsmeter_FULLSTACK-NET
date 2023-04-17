using Microsoft.AspNetCore.Identity;
using Sportsmeter_frontend.Models;

namespace CRUD_Design_Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto apiUserDto);

        Task<AuthResponseDto> Login (LoginDto loginDto);

        Task<string> CreateRefreshToken();
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto authResponseDto);

    }
}
