using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WeatherApp.Pages
{
    public class IndexModel : PageModel
    {
        private string _url = "http://api.weatherapi.com/v1/current.json?key=";
        public Information Info;
        
        private string _result = "";

        public IndexModel()
        {
            Info = new Information {Icon = null};
            string key;
            
            try
            {
                var read = new StreamReader("API.txt");
                key = read.ReadLine();
            }
            catch (Exception)
            {
                key = Environment.GetEnvironmentVariable("ApiKey");
            }

            _url += key;
        }
        
        public async Task OnPostSearch(string city)
        {
            var req = new HttpClient();
            _url += $"&q={city}&aqi=no";
            try
            {
                _result = await req.GetStringAsync(_url);
            }
            catch (HttpRequestException)
            {
                Info.Text = "Please insert a valid city";
                return;
            }
            catch (Exception)
            {
                Info.Text = "Error!";
                return;
            }

            var deserialized = JObject.Parse(_result);
            try
            {
                Info = deserialized["current"]?["condition"]?.ToObject<Information>();
            }
            catch (NullReferenceException)
            {
                RedirectToPage("Index");
            }
        }
    }

    public class Information
    {
        public string Text { get; set; }
        public string Icon { get; init; }
    }
}