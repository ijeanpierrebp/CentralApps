using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoPower.Models;
using ToDoPower.Interfaces.Tenants;

namespace ToDoPower.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ITenantService _tenantService;

    // Constructor modificado para recibir el servicio de tenants
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService): base(options)
    {
        _tenantService = tenantService;
    }

    // Propiedad para acceder f�cilmente al nombre del tenant actual
    public string TenantName => _tenantService.GetTenant()?.TenantName ?? string.Empty;

    // DbSets existentes
    public DbSet<Models.User> Users { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }

    // Sobrescribir OnConfiguring para usar la cadena de conexi�n espec�fica del tenant
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenantConnectionString = _tenantService.GetConnectionString();
        if (!string.IsNullOrEmpty(tenantConnectionString))
        {
            optionsBuilder.UseSqlServer(tenantConnectionString);
        }
    }

    // Sobrescribir OnModelCreating para aplicar filtros de tenant a nivel de modelo
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar filtros de tenant a las entidades que implementan IHasTenant
        // Asumiendo que Task implementa IHasTenant
        modelBuilder.Entity<Models.Task>().HasQueryFilter(t => t.TenantName == TenantName);
        modelBuilder.Entity<Models.User>().HasQueryFilter(t => t.TenantName == TenantName);

        // Aqu� podr�as agregar m�s filtros para otras entidades que implementen IHasTenant
        // Por ejemplo:
        // modelBuilder.Entity<OtraEntidad>().HasQueryFilter(e => e.TenantName == TenantName);

        // Si tienes un m�todo para datos semilla, podr�as llamarlo aqu�
        // SeedData.Seed(modelBuilder);
    }

    // Sobrescribir SaveChangesAsync para establecer autom�ticamente el TenantName en entidades nuevas o modificadas
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Buscar todas las entidades que implementan IHasTenant y est�n siendo a�adidas o modificadas
        ChangeTracker.Entries<ITenant>()
            .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified)
            .ToList()
            .ForEach(entry => entry.Entity.TenantName = TenantName);

        return await base.SaveChangesAsync(cancellationToken);
    }
}