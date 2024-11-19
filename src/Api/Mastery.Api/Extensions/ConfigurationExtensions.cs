namespace Mastery.Api.Extensions;

internal static class ConfigurationExtensions
{
    internal static void AddModulesConfiguration(this IConfigurationBuilder configurationBuilder, params IReadOnlyCollection<string> modules)
    {
        foreach (string module in modules)
        {
            configurationBuilder.AddJsonFile($"modules.{module}.json", optional: false, reloadOnChange: true);
            configurationBuilder.AddJsonFile($"modules.{module}.Development.json", optional: true, reloadOnChange: true);
        }
    }
}
