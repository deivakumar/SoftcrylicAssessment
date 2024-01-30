using System.ComponentModel.DataAnnotations;

namespace HospitalAssessment.Shared.Patients
{
    public class PatientProgressNoteCommand
    {
        [Required]
        public string SectionName { get; set; }
        [Required]
        public string SectionText { get; set; }
        public int Index { get; set; }
    }
}
