using System;
using System.Collections.Generic;
using System.Linq;
using SampleApi.Api.Models;
using SampleApi.Options;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Digipolis.Toolbox.Eventhandler;
using SampleApi.Business;
using SampleApi.Entities;

namespace SampleApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : Controller
    {
        public ExampleController(IEventHandler eventHandler, IMyDemoEntityBL bl, IOptions<AppSettings> appSettings)
        {
            EventHandler = eventHandler;
            MyDemoEntityBusiness = bl;

            // This is an example of how you inject configuration options that are read from config files and registered in Startup.cs.
            AppSettings = appSettings.Value;
        }

        public IEventHandler EventHandler { get; private set; }
        public IMyDemoEntityBL MyDemoEntityBusiness { get; private set; }

        public AppSettings AppSettings { get; private set; }

     
        // GET /api/example
        [HttpGet]
        public IActionResult GetAll()
        {

                                 

            // this will return a HTTP Status Code 200 (OK) along with the data
            return Ok(MyDemoEntityBusiness.GetAll());
        }

        // GET /api/example/2
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = MyDemoEntityBusiness.Get(id);

            if (result == null )
            {       

                // this will return a HTTP Status Code 404 (Not Found) along with the message
                return HttpNotFound($"No example found with id = {id}");
            }
            else
            {
                // this will return a HTTP Status Code 200 (OK) along with the data
                return Ok(result);
            }

        
        }

        [HttpPost]
        public IActionResult Save([FromBody] MyDemoEntity myentity)
        {         

            return Ok(MyDemoEntityBusiness.Save(myentity)); //Return code is not ok, but you get the point...
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            MyDemoEntityBusiness.Delete(id);

            return Ok(); //Return code is not ok, but you get the point...
        }

    }
}
