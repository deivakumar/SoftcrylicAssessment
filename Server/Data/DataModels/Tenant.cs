using System.ComponentModel.DataAnnotations;

namespace HospitalAssessment.Server.Data.DataModels
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }
        public string TenantName { get; set; }
    }
}
