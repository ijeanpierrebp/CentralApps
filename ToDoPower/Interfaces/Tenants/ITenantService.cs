using ToDoPower.Configuration;

namespace ToDoPower.Interfaces.Tenants;

public interface ITenantService
{
    public Tenant GetTenant();
    public string GetConnectionString();
}
