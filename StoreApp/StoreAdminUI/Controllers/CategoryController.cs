using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreAdminUI.ViewModels;
using System.Text;

namespace StoreAdminUI.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index(int page=1)
        {
            string url = "https://localhost:7096/admin/api/categories?page="+page;
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                PaginatedVM<CategoryListItemVM> model = JsonConvert.DeserializeObject<PaginatedVM<CategoryListItemVM>>(content);

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(model));
                return View(model);
            }

            return View("Error");
        }
    }
}
