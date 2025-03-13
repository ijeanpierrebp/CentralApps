namespace ToDoPower.Interfaces.Tenants;

public interface ITenant
{
    public Guid TenantID { get; set; }
    public string TenantName { get; set; }
}
