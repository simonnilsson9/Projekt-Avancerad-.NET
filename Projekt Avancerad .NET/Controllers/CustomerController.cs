using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Projekt_Avancerad_.NET.DTO;
using Projekt_Avancerad_.NET.Interfaces;


namespace Projekt_Avancerad_.NET.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer _customerRepository;
        private ILogin _login;
        private readonly IMapper _mapper;        
        
        public CustomerController(ICustomer customerRepo ,IMapper mapper, ILogin login)
        {
            _customerRepository = customerRepo;
            _login = login;
            _mapper = mapper;                        
        }

        [Authorize(Roles = "admin, company")]
        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(string name, string phone, string email, string password, string role)
        {
            try
            {
                var existingLoginInfo = await _login.GetByEmail(email);
                if (existingLoginInfo != null)
                {
                    return BadRequest("Email is already in use.");
                }

                CustomerDTOAdd newCustomer = new CustomerDTOAdd()
                {
                    Name = name,
                    Phone = phone,
                    EMail = email,
                };

                var customer = _mapper.Map<Customer>(newCustomer);

                if (newCustomer != null)
                {
                    var addedCustomer = await _customerRepository.Add(customer);

                    LoginInfo newLoginInfo = new LoginInfo()
                    {
                        EMail = email,
                        Password = password,
                        Role = role,
                        CustomerId = addedCustomer.CustomerId
                    };

                    await _login.Add(newLoginInfo);
                    return Ok(addedCustomer);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to put data to database.");
            }
        }

        [Authorize(Roles = "admin, company")]
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(int id, string name, string phone, string email)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetById(id);
                if (existingCustomer == null)
                {
                    return NotFound($"Customer with ID: {id} was not found.");
                }

                var existingLoginInfo = await _login.GetByEmail(existingCustomer.Email);
                if (existingLoginInfo == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Associated login info not found.");
                }

                existingCustomer.Name = name;
                existingCustomer.Phone = phone;
                existingCustomer.Email = email;

                // Uppdatera login-info om e-postadressen ändras
                if (!string.Equals(existingLoginInfo.EMail, email, StringComparison.OrdinalIgnoreCase))
                {
                    existingLoginInfo.EMail = email;
                    await _login.Update(existingLoginInfo);
                }

                await _customerRepository.Update(existingCustomer);
                var updatedCustomerDTO = _mapper.Map<CustomerDTOAdd>(existingCustomer);

                return Ok(updatedCustomerDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update data to database.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var customerToDelete = await _customerRepository.GetById(id);
                if (customerToDelete == null)
                {
                    return NotFound($"Customer with ID: {id} was not found.");
                }

                var loginInfoToDelete = await _login.GetByCustomerId(id);
                if (loginInfoToDelete != null)
                {
                    await _login.Delete(loginInfoToDelete.LoginId);
                }

                var deleteResult = await _customerRepository.Delete(id);
                var deletedCustomerDTO = _mapper.Map<CustomerDTO>(deleteResult);
                return Ok(deletedCustomerDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting customer:");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAll();
                var customersDTO = (customers.Select(c => _mapper.Map<CustomerDTO>(c)));
                return Ok(customersDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("CustomersAndAppointments")]
        public async Task<IActionResult> GetCustomerAndAppointments(int id)
        {
            try
            {
                var customer = await _customerRepository.GetById(id);
                if (customer != null)
                {
                    return Ok(customer);
                }
                return NotFound($"The customer with appointmentId: {id} could not be found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("Sort/Filter")]
        public async Task<IActionResult> GetAllCustomers(
        string? filterByName = null,
        string? sortBy = "Name",
        string sortOrder = "asc")
        {
            try
            {
                var customers = await _customerRepository.GetAll();

                //Filtering
                if (!string.IsNullOrEmpty(filterByName))
                {
                    customers = customers.Where(c => c.Name.Contains(filterByName, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                //Sorting
                customers = sortOrder.ToLower() switch
                {
                    "desc" => sortBy.ToLower() switch
                    {
                        "name" => customers.OrderByDescending(c => c.Name).ToList(),
                        "customerid" => customers.OrderByDescending(c => c.CustomerId).ToList(),
                        _ => customers.OrderByDescending(c => c.Name).ToList(),
                    },
                    _ => sortBy.ToLower() switch
                    {
                        "name" => customers.OrderBy(c => c.Name).ToList(),
                        "customerid" => customers.OrderBy(c => c.CustomerId).ToList(),
                        _ => customers.OrderBy(c => c.Name).ToList(),
                    }
                };

                var customersDTO = customers.Select(c => _mapper.Map<CustomerDTO>(c));
                return Ok(customersDTO);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database.");
            }
        }

    }
}
