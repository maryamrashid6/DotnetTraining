using static TodoApi.Services.Dtos.UserDto;
using TodoApi.Entities;
using TodoApi.Services.Dtos;

namespace TodoApi.Services.Contracts
{
    public interface IAuthService
    {
        User Register(UserAddRequestDto user);
        User Authenticate(string email, string password);
        string GenerateJwtToken(User user);
    }
}
