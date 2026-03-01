using home_control_api.DTOs;
using home_control_api.Interfaces;
using home_control_api.Models;

namespace home_control_api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(users => new UserResponseDto
        {
            UserId = users.UserId,
            Name = users.Name,
            Age = users.Age
        });
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Age = dto.Age
        };

        var createdUser = await _userRepository.CreateAsync(user);

        return new UserResponseDto { UserId = createdUser.UserId, Name = createdUser.Name, Age = createdUser.Age };
    }

    public async Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado.");

        user.Name = dto.Name;
        user.Age = dto.Age;

        var updatedUser = await _userRepository.UpdateAsync(user);
        return new UserResponseDto { UserId = updatedUser.UserId, Name = updatedUser.Name, Age = updatedUser.Age };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

}
