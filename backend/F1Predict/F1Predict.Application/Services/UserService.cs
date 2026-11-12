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

    public Task<UserDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> CreateAsync(UserEditDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, UserEditDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}