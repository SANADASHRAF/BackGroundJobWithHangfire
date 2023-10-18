using Hangfire;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using System;
using System.Globalization;
using TimeZoneConverter;

namespace BackGroundJobWithHangfire.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        

       
        //install hangfire 
        //add it's servese in program 
        //add connection string and then add it with it's servese in program 
        //then use hangfire in endpoint or in program "as your need "
       


        [HttpGet(Name = "TestBackgroundJob")]
        public IActionResult TestBackgroundJob()
        {
            Console.WriteLine(DateTime.Now);
            BackgroundJob.Enqueue(() => sentemail("sanad@gmail.com")); // اى تاسك بتديهالها بتتنفذ اول متستدعيها علطول فى endpoint دى
            BackgroundJob.Schedule(() => sentemail("sanad@gmail.com"),TimeSpan.FromDays(1)); //تاسك هيتنفذ بعد وقت معين 
            RecurringJob.AddOrUpdate(() => sentemail("sanad@gmail.com"), Cron.Daily());// repeted as period you selected 

            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi =true)]
        public void  sentemail(string email)
        {
            Console.WriteLine($"sent at { DateTime.Now } ");
        }

       

    }
}