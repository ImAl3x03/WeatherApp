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
        // URL where i call the API
        private string _url = "http://api.weatherapi.com/v1/current.json?key=";
        //Information class about the weather
        public Information Info { get; private init; }
        //Location class about the weather
        public Location Location { get; private set; }
        
        //String where's the response JSON sent by API
        private string _result = "";

        public IndexModel()
        {
            Info = new Information();
            string key;
            
            /*
             * Take the API key:
             *  - If there's the file (localhost) it take the key from the file
             *  - If there isn't the file (Server) it the the key from Environment variable
             */
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
            //Call the API and take the JSON
            using (var req = new HttpClient())
            {
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
            }

            //Deserializing JSON and taking the information that are stored in the class
            var deserialized = JObject.Parse(_result);
            try
            {
                Info.TempC = deserialized["current"]?["temp_c"]?.ToObject<double>();
                Info.Text = deserialized["current"]?["condition"]?["text"]?.ToString();
                Info.Icon = deserialized["current"]?["condition"]?["icon"]?.ToString();
                Location = deserialized["location"]?.ToObject<Location>();
            }
            catch (NullReferenceException)
            {
                RedirectToPage("Index");
            }
            catch (InvalidOperationException)
            {
                RedirectToPage("Index");
            }
        }
    }

    //Class information where there's the weather information
    public class Information
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public double? TempC { get; set; }
    }

    //Class location where there's the location chosen by the user
    public class Location
    {
        public Location(string name, string region, string country)
        {
            Name = name;
            Region = region;
            Country = country;
        }

        private string Name { get; set; }
        private string Region { get; set; }
        private string Country { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Region}, {Country}";
        }
    }
}