using Assignment2.Model;
using Assignment2.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

    namespace Assignment2.Controllers
    {
        [Route("api/1.0/Employee")]
        [ApiController]
        [Authorize]
        public class EmployeeController : ControllerBase
        {
            private readonly IEmployeeService _iEmployeeServide;
            public EmployeeController(IEmployeeService iEmployeeServide)
            {
                _iEmployeeServide = iEmployeeServide;
            }



            [HttpGet]
            public IActionResult Get()
            {
                var employees = _iEmployeeServide.GetAll();
                return Ok(employees);
            }

            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                try
                {
                    var employee = _iEmployeeServide.Get(id);
                    if (employee == null)
                    {
                        return NotFound("Employee with id =" + id + " is not found");
                    }
                    else
                    {
                        return Ok(employee);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


            [HttpPost]
            public IActionResult Post(Employee employee)
            {

                try
                {
                    var emp = _iEmployeeServide.Save(employee);

                    var url = Request.Scheme + "://" + Request.Host.Value;

                    var uri = new Uri(url + emp.Id.ToString());
                    return Created(uri, emp);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


            [HttpPut("{id}")]
            public IActionResult Put(int id, Employee employee)
            {
                try
                {
                    var emp = _iEmployeeServide.Get(id);
                    if (emp == null)
                    {
                        return NotFound("Employee with Id = " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        _iEmployeeServide.Update(id, employee);
                        return Ok(employee);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }


            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                try
                {
                    var employee = _iEmployeeServide.Get(id);
                    if (employee == null)
                    {
                        return NotFound("Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        _iEmployeeServide.Delete(id);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }

