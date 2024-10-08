using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CarLib;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private CarRepository _carsRepository;

        public CarsController(CarRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }


        // GET: api/<CarsController>
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            return _carsRepository.GetAll();
        }

        // GET api/<CarsController>/5

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]

        public ActionResult<Car> Get(int id)
        {
            Car? car = _carsRepository.GetByID(id);
            if (car == null)
            {
                return NotFound("No such item, id: " + id);
            }
            else
            {
                return Ok(car);
            }
        }


        // POST api/<CarsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car newCar)
        {
            try
            {
                Car createdCar = _carsRepository.Add(newCar);
                return Created("/" + createdCar.Id, createdCar);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CarsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Car> Put(int id, [FromBody] Car updatedCar)
        {
            try
            {
                Car? car = _carsRepository.Update(id, updatedCar);
                if (car != null)
                {
                    return Ok(car);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public Car? Delete(int id)
        {
            return _carsRepository.Delete(id);
        }

        [HttpGet]
        public ActionResult<Car> Amount([FromHeader]string? amount)
        {
            if(amount == null)
            {
                return Ok("ikke noget amoubt men det ok");
            }
            if (int.TryParse(amount, out int parsedHej))
            {
                Response.Headers.Add("Kesi", "888");
                return Ok("Du har sendt: " + parsedHej);
            }
            else
            {
                return BadRequest("Hej header skal være en int!");
            }

        }

    }

}

