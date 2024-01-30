using AutoMapper;
using HospitalAssessment.Server.Data.DataModels;
using HospitalAssessment.Shared.Patients;

namespace HospitalAssessment.Server.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientCommand>().ReverseMap();
            CreateMap<PatientProgressNote, PatientProgressNoteCommand>().ReverseMap();
        }
    }
}
