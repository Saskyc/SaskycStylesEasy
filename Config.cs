using Exiled.API.Interfaces;

namespace SaskycStylesEasy;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;

    public bool Debug { get; set; } = false;
}