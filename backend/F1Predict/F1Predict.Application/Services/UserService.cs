using F1Predict.Application.Interfaces;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;
using F1Predict.Application.Mapping;

namespace F1Predict.Application.Services;

public class UserService(IRepository<User> userRepository) : IUserService
{
    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();
        return users.Select(u=>u.ToDto()).ToList();
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return user?.ToDto();
    }

    public async Task<UserDto> CreateAsync(UserEditDto dto)
    {
        var created = await userRepository.AddAsync(dto.ToEntity());
        return created.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, UserEditDto updatedDto)
    {
        var client = updatedDto.ToEntity();
        client.Id = id;

        return await userRepository.UpdateAsync(client);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return userRepository.DeleteAsync(id);
    }
}