using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SigortamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        private Context _context;
        public CustomerController(Context context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public List<Customer> Get()
        {
             return _context.Customers.ToList();
        }
        [HttpGet("{TCKN}")]
        public Customer Get(long TCKN)
        {
            //log for api.
            _logger.LogInformation("Customer Get Api called." + DateTime.Now.ToString());
            return _context.Customers.Where(x=>x.TCKN == TCKN).FirstOrDefault();
        }
        [HttpPost("CustomerAdd")]
        public async Task<IActionResult> CustomerAdd([FromBody] Customer customer)
        {
            //mernisservice bağlanma ve eklenecek olan kullanıcıyı kontrol etme
            bool result = await CheckTCKimlik(customer);
            if (result)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                _logger.LogInformation("New customer added to database" + DateTime.Now.ToString());
                return Ok();
            }
            _logger.LogError("Error while adding new customer" + DateTime.Now.ToString());
            return BadRequest("Tckimlik doğrulanamadı.");
        }

        private static async Task<bool> CheckTCKimlik(Customer customer)
        {
            var client = new MernisService.KPSPublicSoapClient(MernisService.KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var response = await client.TCKimlikNoDogrulaAsync(Convert.ToInt64(customer.TCKN), customer.Name, customer.Surname, customer.Birthdate);
            var result = response.Body.TCKimlikNoDogrulaResult;
            return result;
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            //mernisservice bağlanma ve eklenecek olan kullanıcıyı kontrol etme
            bool result = await CheckTCKimlik(customer);
            if (result)
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
                _logger.LogInformation("A customer successfully updated" + DateTime.Now.ToString());
                return Ok();
            }
            _logger.LogError("Error while updated a customer" + DateTime.Now.ToString());
            return BadRequest("Böyle bir kullanıcı bulunamadı.");
        }
        [HttpDelete("{TCKN}")]
        public void CustomerDelete(long TCKN)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.TCKN == TCKN);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            _logger.LogInformation("A customer successfully deleted" + DateTime.Now.ToString());
        }
    }
}
