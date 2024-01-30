

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAssessment.Server.Data.DataModels
{

    public class PatientProgressNote
    {
        [Key]
        public int ProgressNoteId { get; set; }
        public int VisitId { get; set; }
        [ForeignKey("VisitId")]
        public virtual PatientVisit PatientVisit { get; set; }
        public string SectionName { get; set; }
        public string SectionText { get; set; }
    }
}
