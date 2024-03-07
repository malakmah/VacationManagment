using Microsoft.AspNetCore.Mvc;
using VacationManagement.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VacationManagement.ApiControllers
{
    [Route("api/VacationPlansApi")]
    [ApiController]
    public class VacationPlansApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VacationPlansApiController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/<VacationPlansController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VacationPlansController>/5
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                return Ok(_context.Employees.Where(X=>X.Name.Contains(name)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

        // POST api/<VacationPlansController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VacationPlansController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationPlansController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
