using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeatherApp.Pages
{
    public class IndexModel : PageModel
    {
        private string _url = "http://api.weatherapi.com/v1/current.json?key=";

        public async Task OnPostSearch(string city)
        {
            var req = new HttpClient();
            _url += "&q=" + city + "&aqi=no";
            var result = await req.GetStringAsync(_url);
        }
    }
}