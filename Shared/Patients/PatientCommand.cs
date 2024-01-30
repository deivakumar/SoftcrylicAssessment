using System.ComponentModel.DataAnnotations;

namespace HospitalAssessment.Shared.Patients
{
    public class PatientCommand
    {
        public int PatientId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        public virtual PatientVisitCommand[] Visits { get; set; } = Array.Empty<PatientVisitCommand>();
    }
}
