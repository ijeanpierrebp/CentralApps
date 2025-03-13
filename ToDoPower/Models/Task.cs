using System.ComponentModel.DataAnnotations;
using ToDoPower.Interfaces.Tenants;

namespace ToDoPower.Models;

public class Task:ITenant
{
    [Key]
    public Guid TaskId { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ExpireOn { get; set; }
    public bool IsCompleted { get; set; }
    public Guid TenantID { get; set; }   
    public string TenantName { get; set; }   
}
