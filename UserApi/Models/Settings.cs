using UserApi.Repositories.Entities;

namespace UserApi.Models;

public record Settings(
    int MaxConnectedDevices,
    string Theme
)
{
    public static Settings From(SettingsEntity settingsEntity)
    {
        return new Settings(settingsEntity.MaxConnectedDevices, settingsEntity.Theme);
    }
}