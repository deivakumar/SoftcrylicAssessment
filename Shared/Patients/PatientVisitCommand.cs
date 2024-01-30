namespace HospitalAssessment.Shared.Patients
{
    public class PatientVisitCommand
    {
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public PatientProgressNoteCommand[] ProgressNotes { get; set; }
    }
}
