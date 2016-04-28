using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Totler1C.BLL.Interfaces;
using Totler1C.DAL.Models;
using Trade_MVC6.Attributes;
using Trade_MVC6.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trade_MVC6.Areas.Api._1C
{
    [Route("api/1C/Users"), Authorize(Roles = "Admin"), ValidateHeaderAntiForgeryToken, AjaxValidate]
    public class Users1C : Controller
    {

        private readonly IProvider1C _provider1C;

        public Users1C(IProvider1C provider1C)
        {
            _provider1C = provider1C;
        }

        // GET: api/values
        [HttpGet]
        public Task<ICollection<User1C>> Get()
        {
            return _provider1C.Users.QueryAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
