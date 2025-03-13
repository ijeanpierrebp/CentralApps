using ToDoPower.Interfaces.Tenants;

namespace ToDoPower.Configuration;

public class TenantSettings
{
    public string? DefaultConnectionString { get; set; }
    public List<Tenant>? Tenants { get; set; }
}
