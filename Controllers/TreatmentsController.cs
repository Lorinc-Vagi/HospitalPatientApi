using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalPatientApi.Context;
using HospitalPatientApi.Entities;

namespace HospitalPatientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly HospitalContext context;

        public TreatmentsController(HospitalContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("create-rental")]
        public async Task<ActionResult<Treatment>> createTreatment([FromBody] Treatment treatment)
        {
            context.Treatments.Add(treatment);
            await context.SaveChangesAsync();
            return Ok(treatment);
        }
        [HttpGet]
        [Route("get-rentals")]
        public async Task<ActionResult<IEnumerable<Treatment>>> getallTreatment()
        {
            return await context.Treatments.ToListAsync();
        }
        [HttpGet]
        [Route("get-rental/{id}")]
        public async Task<ActionResult<Treatment>> getOneTreatment(int id)
        {
            var toDisplay = await context.Treatments.FindAsync(id);
            if (toDisplay is null)
            {
                return NotFound();
            }
            return Ok(toDisplay);
        }
        [HttpPut]
        [Route("update-rental/{id}")]
        public async Task<IActionResult> updateRental(int id, [FromBody] Treatment treatment)
        {
            if (id != treatment.Id)
            {
                return BadRequest();
            }
            if (!context.Treatments.Any(b => b.Id == id))
            {
                return NotFound();
            }
            context.Entry(treatment).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-rental/{id}")]
        public async Task<IActionResult> deleteTreatment(int id)
        {
            var toDelete = await context.Treatments.FindAsync(id);
            if (toDelete is null)
            {
                return NotFound();
            }
            context.Treatments.Remove(toDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
