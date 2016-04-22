using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Trade_MVC6.Attributes;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.Services;
using Trade_MVC6.ViewModels.Admin;
using Microsoft.Data.Entity;
using Trade_MVC6.Models.B2BStrore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trade_MVC6.Areas.Api
{
    [Route("api/Users"), Authorize(Roles = "Admin"), ValidateHeaderAntiForgeryToken, AjaxValidate]
    public class Users : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProvider1C _provider1C;
        private readonly IMapper _mapper;
        private readonly B2BDbContext _db;

        public Users(UserManager<ApplicationUser> userManager, IProvider1C provider1C, IMapper mapper, B2BDbContext db)
        {
            _userManager = userManager;
            _provider1C = provider1C;
            _mapper = mapper;
            _db = db;
        }

        // GET: api/values
        [HttpGet]
        public Task<IEnumerable<UserViewModel>> Get() => Task.Run(() =>
        {
            IEnumerable<UserViewModel> users = new List<UserViewModel>();
            var userBuf = _db.Users.Include(d => d.Contact).ToList();// _userManager.Users.Include(d => d.Contact).ToList()
            _mapper.Map(userBuf, users);
            return users;
        });

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
