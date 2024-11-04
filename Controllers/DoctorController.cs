using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalPatientApi.Context;
using HospitalPatientApi.Entities;

namespace HospitalPatientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly HospitalContext context;

        public DoctorController(HospitalContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("create-doctor")]
        public async Task<ActionResult<Doctor>> createDoctor([FromBody] Doctor doc)
        {
            context.Doctors.Add(doc);
            await context.SaveChangesAsync();
            return Ok(doc);
        }
        [HttpGet]
        [Route("get-doctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> getallDoctor()
        {
            return await context.Doctors.ToListAsync();
        }
        [HttpGet]
        [Route("get-doctor/{id}")]
        public async Task<ActionResult<Doctor>> getOneDoctor(int id)
        {
            var toDisplay = await context.Doctors.FindAsync(id);
            if (toDisplay is null)
            {
                return NotFound();
            }
            return Ok(toDisplay);
        }
        [HttpPut]
        [Route("update-doctor/{id}")]
        public async Task<IActionResult> updateDoctor(int id, [FromBody]Doctor doc)
        {
            if (id!=doc.Id)
            {
                return BadRequest();
            }
            if (!context.Doctors.Any(b=>b.Id==id))
            {
                return NotFound();
            }
            context.Entry(doc).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-doctor/{id}")]
        public async Task<IActionResult> deleteDoctor(int id)
        {
            var toDelete = await context.Doctors.FindAsync(id);
            if (toDelete is null)
            {
                return NotFound();
            }
            context.Doctors.Remove(toDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
