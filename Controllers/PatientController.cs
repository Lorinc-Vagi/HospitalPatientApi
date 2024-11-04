using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalPatientApi.Context;
using HospitalPatientApi.Entities;

namespace HospitalPatientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly HospitalContext context;

        public PatientController(HospitalContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("create-patient")]
        public async Task<ActionResult<Patient>> createPatient([FromBody] Patient patient)
        {
            context.Patients.Add(patient);
            await context.SaveChangesAsync();
            return Ok(patient);
        }
        [HttpGet]
        [Route("get-patients")]
        public async Task<ActionResult<IEnumerable<Patient>>> getPatient()
        {
            return await context.Patients.ToListAsync();
        }
        [HttpGet]
        [Route("get-patient/{id}")]
        public async Task<ActionResult<Patient>> getOnePatient(int id)
        {
            var toDisplay = await context.Patients.FindAsync(id);
            if (toDisplay is null)
            {
                return NotFound();
            }
            return Ok(toDisplay);
        }
        [HttpPut]
        [Route("update-patient/{id}")]
        public async Task<IActionResult> updatePatient(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }
            if (!context.Patients.Any(b => b.Id == id))
            {
                return NotFound();
            }
            context.Entry(patient).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-patient/{id}")]
        public async Task<IActionResult> deletePatient(int id)
        {
            var toDelete = await context.Patients.FindAsync(id);
            if (toDelete is null)
            {
                return NotFound();
            }
            context.Patients.Remove(toDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
