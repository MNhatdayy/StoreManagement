using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetData : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public GetData(IHttpClientFactory httpClientFactory) 
        {
            _httpClient = httpClientFactory.CreateClient();

        }

        [HttpPost]
        [Route("Get")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDataOrder([FromBody] RequestData model)
        {
            try
            {

                int idStore = model.IdStore;
                int idTable = model.IdTable;
                List<int> foodItems = model.Food;

                var requestData = new
                {
                    idStore = idStore,
                    idTable = idTable,
                    Food = foodItems
                };
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7251/Owner/ManagementOrder/ReceiveData", requestData);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
                }
                else
                {
                    
                    return StatusCode((int)response.StatusCode);
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class RequestData
    {
        public int IdStore { get; set; }
        public int IdTable { get; set; }
        public List<int> Food { get; set; }
    }

}
