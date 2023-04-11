using EntityLayer;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SigortamNetUI.Validations;
using System.Text;

namespace SigortamNetUI.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            CustomerValidator validationRules = new CustomerValidator();
            ValidationResult result = validationRules.Validate(customer);

            if (result.IsValid)
            {
                try
                {
                    HttpClient client = new HttpClient();

                    var endPoint = "https://localhost:7211/api/customer/CustomerAdd";
                    var newPostJsonPayment = JsonConvert.SerializeObject(customer);
                    var payloadPayment = new StringContent(newPostJsonPayment, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(endPoint, payloadPayment);
                    //var responseBody = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        
                        return Json(true);
                    }
                    return Json(false);
                }
                catch (Exception)
                {

                    return Json(false);
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return Json(false);
        }
        public async Task<IActionResult> GetCustomerById(long tckn)
        {

            try
            {
                HttpClient client = new HttpClient();

                var endPoint = "https://localhost:7211/api/customer/" + tckn;
                var response = await client.GetAsync(endPoint);
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<Customer>(responseBody);
                if (response.IsSuccessStatusCode)
                {

                    return View(responseObject);

                }
                return View(responseObject);
            }
            catch (Exception)
            {

                return Json(false);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                HttpClient client = new HttpClient();

                var endPoint = "https://localhost:7211/api/customer/";
                var response = await client.GetAsync(endPoint);
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<List<Customer>>(responseBody);
                if (response.IsSuccessStatusCode)
                {
                    return View(responseObject);
                }
                return View(responseObject);
            }
            catch (Exception)
            {

                return Json(false);
            }
        }
        public async Task<IActionResult> DeleteCustomer(long tckn)
        {
            try
            {
                HttpClient client = new HttpClient();

                var endPoint = "https://localhost:7211/api/customer/" + tckn;
                var response = await client.DeleteAsync(endPoint);
                //var responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception)
            {

                return Json(false);
            }
        }
    }
}
