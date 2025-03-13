using System.ComponentModel.DataAnnotations;
using ToDoPower.Interfaces.Tenants;

namespace ToDoPower.Models;

public class User:ITenant
{
    [Key]
    public Guid UserId { get; set; }
    public string UserNickName { get; set; }
    public string UserName { get; set; }
    public string UserLastName { get; set; }
    public string UserEmail { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid TenantID { get; set; }
    public string TenantName { get; set; }


}
