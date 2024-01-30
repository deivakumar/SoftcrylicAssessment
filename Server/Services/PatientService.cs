using AutoMapper;
using HospitalAssessment.Server.Data;
using HospitalAssessment.Server.Data.DataModels;
using HospitalAssessment.Shared.Patients;
using Microsoft.EntityFrameworkCore;

namespace HospitalAssessment.Server.Services
{
    public class PatientService : IPatientService
    {
        private readonly HospitalContext _context;
        private readonly IMapper _mapper;
        private readonly int TenantId;

        public PatientService(HospitalContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            TenantId = config.GetValue<int>("TenantId");
        }

        public IEnumerable<Patient> GetPatients(string? searchText = "")
        {
            return _context.Patients
                .Include(p => p.Visits)
                .Where(p => p.TenantId == TenantId)
                .Where(p => !p.IsDeleted)
                .AsEnumerable()
                .Where(p => string.IsNullOrEmpty(searchText) || p.FirstName.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase) || p.LastName.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase));
        }

        public Patient? GetPatientById(int patientId)
        {
            return _context.Patients.FirstOrDefault(p => p.TenantId == TenantId && p.PatientId == patientId);
        }

        public int AddPatient(PatientCommand command)
        {
            var patient = _mapper.Map<Patient>(command);
            patient.TenantId = TenantId;
            patient.CreatedAt = DateTime.Now;

            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient.PatientId;
        }

        public bool UpdatePatient(PatientCommand command)
        {
            var pat = GetPatientById(command.PatientId);
            if (pat == null) return false;

            var patient = _mapper.Map(command, pat);
            patient.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeletePatient(int patientId)
        {
            var pat = GetPatientById(patientId);
            if (pat == null) return false;

            pat.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public int AddPatientProgress(int patientId, PatientProgressNoteCommand[] command)
        {
            var pat = GetPatientById(patientId);
            if (pat == null) return 0;

            var notes = _mapper.Map<PatientProgressNote[]>(command);
            var visit = new PatientVisit { VisitDate = DateTime.Now, ProgressNotes = notes };
            pat.Visits.Add(visit);

            _context.SaveChanges();
            return visit.VisitId;
        }
    }
}
