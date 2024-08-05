namespace Mastery.Modules.Users.Infrastructure;

public sealed class IdentityModuleOptions
{
    public const string SectionName = "IdentityModule";
    
    public DatabaseOptions Database { get; set; }
}

public sealed class DatabaseOptions
{
    public string ConnectionString { get; set; }
    
    public string MigrationsHistoryTable { get; set; }
}
