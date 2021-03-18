using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    public class AsyncAwaitController : Controller
    {
        // GET: AsyncAwait
        public ActionResult Index()
        {
            return View();
        }

        public string GetLocation()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            string country = GetCountry();
            string city = GetCity();

            watch.Stop();

            return $"{country}-{city}-{watch.ElapsedMilliseconds.ToString()} ms";
        }

        private string GetCountry()
        {
            Thread.Sleep(1000);
            return "Vietnam";
        }

        private string GetCity()
        {
            Thread.Sleep(2000);
            return "Ho Chi Minh";
        }


        public async Task<string> GetLocationAsync()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var countryTask = GetCountryAsync();
            var cityTask = GetCityAsync();

            var country = await countryTask;
            var city = await cityTask;

            watch.Stop();
            return $"{country}-{city}-{watch.ElapsedMilliseconds.ToString()} ms";
        }


        private async Task<string> GetCountryAsync()
        {
            //Thread.Sleep(3000);
            await Task.Delay(1000);
            return "Vietnam";
        }

        private async Task<string> GetCityAsync()
        {
            //Thread.Sleep(4000);
            await Task.Delay(2000);
            return "Ho Chi Minh";
        }

    }
}