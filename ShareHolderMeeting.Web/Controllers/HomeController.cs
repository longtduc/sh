using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult ThrowException()
        {
            throw new Exception("This is unhandled exception");
        }

        public ActionResult NullReferenceException()
        {
            throw new NullReferenceException();
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