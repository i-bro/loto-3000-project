using LotoApp.DTOs;

namespace LotoApp.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<string> LoginUserAsync(LoginUserDto loginUserDto);
    }
}
