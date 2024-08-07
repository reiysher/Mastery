namespace Mastery.Modules.Identity.Infrastructure.Persistence.Seeds;

internal sealed class DefaultUser
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string CountryCode { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Password { get; set; }
    
    public string[] Roles { get; set; }
}
