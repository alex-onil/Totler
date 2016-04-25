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
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trade_MVC6.Areas.Api
{
    [Route("api/[controller]"), Authorize(Roles = "Admin"), ValidateHeaderAntiForgeryToken, AjaxValidate]
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
        public Task<IList<UserViewModel>> Get() => Task.Run(() =>
        {
            IList<UserViewModel> users = new List<UserViewModel>();
            var userBuf = _db.Users.Include(d => d.Contact).ToList();// _userManager.Users.Include(d => d.Contact).ToList()
            userBuf.ForEach(el => users.Add(_mapper.Map<ApplicationUser, UserViewModel>(el, opt => opt.AfterMap(
                (s, d) => d.Access1C = _userManager.IsInRoleAsync(s, Roles.User1C).Result))));
            //_mapper.Map<ApplicationUser, UserViewModel>(userBuf, users);
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

        // POST api/values
        [HttpPost("{id:minlength(1)}")]
        public async Task<IActionResult> Activate(string id, string id1C)
        {
            //if (string.IsNullOrEmpty(id)) HttpBadRequest();
            
            var user1C = (await _provider1C.Users.QueryAsync()).First(m => m.Id.Equals(id1C));
            var destUser = await _userManager.FindByIdAsync(id);

            if (user1C != null && destUser != null)
            {
                try
                {
                    await _userManager.Activate1CAccessAsync(destUser, user1C);
                }
                catch (Exception ex)
                {
                    return HttpBadRequest(new[] { "В процессе активации произошла ошибка." });
                }
                var user = _mapper.Map<ApplicationUser, UserViewModel>(destUser, opt => opt.AfterMap(
                                 (s, d) => d.Access1C = _userManager.IsInRoleAsync(s, Roles.User1C).Result));

                return Ok(user);
            }

            return HttpBadRequest(new [] { "В процессе активации произошла ошибка."});
            }

        // POST api/values/Deactivate
        [HttpPost("{id:minlength(1)}/Deactivate")]
        public async Task<IActionResult> Deactivate(string id)
            {

            var destUser = await _userManager.FindByIdAsync(id);

            if (destUser != null)
                {
                try
                    {
                     await _userManager.Deactivate1CAccessAsync(destUser);
                    }
                catch (Exception ex)
                    {
                    return HttpBadRequest(new[] { "В процессе активации произошла ошибка." });
                    }
                var user = _mapper.Map<ApplicationUser, UserViewModel>(destUser, opt => opt.AfterMap(
                                 (s, d) => d.Access1C = _userManager.IsInRoleAsync(s, Roles.User1C).Result));

                return Ok(user);
                }

            return HttpBadRequest(new[] { "В процессе активации произошла ошибка." });
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
