
using System.ComponentModel.DataAnnotations;

namespace HospitalAssessment.Shared.Patients
{
    public class LoginCommand
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
    }
}
