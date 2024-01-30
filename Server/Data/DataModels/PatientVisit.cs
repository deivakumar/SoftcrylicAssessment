

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAssessment.Server.Data.DataModels
{
    public class PatientVisit
    {
        [Key]
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        public DateTime VisitDate { get; set; }
        public ICollection<PatientProgressNote> ProgressNotes { get; set; }
    }
}
