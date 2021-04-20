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
	/*
	* TODO
	* Modify Info from variable to prop
	*/

	/*
	* TODO
	* Take other information from JSON
	* TODO
	* Modify Information class
	* TODO
	* Show the new Information
	* TODO
	* Remove initialize Information from ctor
	*/

        private string _url = "http://api.weatherapi.com/v1/current.json?key=";
        public Information Info { get; private init; }
        public Location Location { get; private set; }
        
        private string _result = "";

        public IndexModel()
        {
            Info = new Information();
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

    public class Information
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public double? TempC { get; set; }
    }

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