using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeatherApp.Pages
{
    public class IndexModel : PageModel
    {
        public async Task OnPostSearch(string city)
        {
            await Task.Delay(500);
        }
    }
}