using HospitalAssessment.Server.Data.DataModels;
using HospitalAssessment.Server.Services;
using HospitalAssessment.Shared.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAssessment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _patientService;
        
        public PatientsController(ILogger<PatientsController> logger, IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public IActionResult Get(string? searchText = "")
        {
            IEnumerable<Patient>  pats = new List<Patient>();
            try
            {
               pats = _patientService.GetPatients(searchText);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Ok(pats);
        }

        //GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Patient? res;
            try
            {
                res = _patientService.GetPatientById(id);
                if (res == null) return NotFound("Invalid Patient ID");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem("Request failed");
            }
            
            return Ok(res);
        }

        // POST api/<PatientsController>
        [HttpPost]
        public IActionResult Post([FromBody] PatientCommand command)
        {
            int res;
            try
            {
                res = _patientService.AddPatient(command);
                if (res == 0) return Problem("Request failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem("Request failed");
            }

            return Ok(res);
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PatientCommand command)
        {
            bool res;
            try
            {
                res = _patientService.UpdatePatient(command);
                if (!res) return Problem("Request failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem("Request failed");
            }

            return Ok(res);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool res;
            try
            {
                res = await _patientService.DeletePatient(id);
                if (!res) return Problem("Request failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem("Request failed");
            }

            return Ok(res);
        }
        
        // POST api/<PatientsController>/5/progress
        [HttpPost("{patientId}/progress")]
        public IActionResult AddProgressNotes(int patientId, [FromBody] PatientProgressNoteCommand[] command)
        {
            int res;
            try
            {
                res = _patientService.AddPatientProgress(patientId, command);
                if (res == 0) return Problem("Request failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem("Request failed");
            }

            return Ok(res);
        }
    }
}
