using HospitalAssessment.Server.Data.DataModels;
using HospitalAssessment.Shared.Patients;

namespace HospitalAssessment.Server.Services
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetPatients(string? searchText = "");
        Patient? GetPatientById(int patientId);
        int AddPatient(PatientCommand command);
        bool UpdatePatient(PatientCommand command);
        Task<bool> DeletePatient(int patientId);
        int AddPatientProgress(int patientId, PatientProgressNoteCommand[] command);
    }
}
