using DarkSide;
using Doggies.Models.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Doggies.Controllers.Abstract;
using Doggies.Models;

namespace WepApp.Controllers.Api
{
    
    [RoutePrefix("api/dog")]
    public class DogController : BaseApiController
    {
        [Route("getDogs")]
        public async Task<IHttpActionResult> getDogs()
        {
            List<Dog> allDogs = await DogManager.GetDogs();
            return WrapSuccess(allDogs);
        }
        protected DogManager DogManager
        {
            get
            {
                return Request.GetOwinContext().Get<DogManager>();
            }
        }
    }
}