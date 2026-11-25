using System.Reflection;

namespace POS;

internal static class AppVersion
{
    internal static string Display { get; } = GetDisplayVersion();

    private static string GetDisplayVersion()
    {
        string? informationalVersion = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion;

        if (!string.IsNullOrWhiteSpace(informationalVersion))
        {
            string version = informationalVersion.Split('+')[0].Split('-')[0];
            string[] parts = version.Split('.');
            if (parts.Length >= 2 &&
                int.TryParse(parts[0], out int major) &&
                int.TryParse(parts[1], out int minor))
            {
                return $"{major}.{minor}";
            }
        }

        Version? assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
        return assemblyVersion is null
            ? "1.1"
            : $"{assemblyVersion.Major}.{assemblyVersion.Minor}";
    }
}
