using UserApi.Models;
using UserApi.Repositories;

namespace UserApi.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly SettingsRepository _settingsRepository;

    public UserService(UserRepository userRepository, SettingsRepository settingsRepository)
    {
        _userRepository = userRepository;
        _settingsRepository = settingsRepository;
    }

    public async Task<User> GetOrCreateUser(string userId, string refreshToken)
    {
        return (await GetUserById(userId) ?? await CreateUser(userId, refreshToken))!;
    }
    
    public async Task<User?> CreateUser(string userId, string refreshToken)
    {
        if (await GetUserById(userId) != null)
        {
            return null;
        }

        var userEntity = await _userRepository.Create(userId, refreshToken);
        var settings = await _settingsRepository.CreateForUser(userId);
        
        return new User(userEntity.Id, settings);
    }

    public async Task<User?> GetUserById(string userId)
    {
        var userEntity = await _userRepository.GetById(userId);

        if (userEntity == null)
        {
            return null;
        }

        var prev = await _settingsRepository.GetSettingsByUserId(userId);
        var settings = new Settings(prev!.MaxConnectedDevices, prev.Theme);
        
        return new User(userEntity.Id, settings);
    }

    public async Task<User?> UpdateUser(string userId, Settings values)
    {
        var settings = await _settingsRepository.UpdateForUser(userId, values.MaxConnectedDevices, values.Theme);

        return settings == null ? null : new User(userId, settings);
    }

    public async Task<bool> DeleteUserById(string userId)
    {
        await _settingsRepository.DeleteSettingsByUserId(userId);
        return await _userRepository.Delete(userId);
    }
}