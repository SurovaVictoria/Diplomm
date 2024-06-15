using Diplomm.Areas.Account.Models;
using Diplomm.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diplomm.Areas.Account.Controllers
{
    [Area("Account")]
    public class RoleController : Controller
    {
        RoleManager<ApplicationRole> _manager;
        UserManager<EmployeesTable> _users;
        public RoleController(RoleManager<ApplicationRole> manager, UserManager<EmployeesTable> users)
        {
            _manager = manager;
            _users = users;
        }

        [Route("{area}/{action}")]
        [HttpGet]
        public IActionResult Role()
        {
            return View(_manager.Roles);
        }

        [Route("{area}/{action}")]
        [HttpPost]
        public IActionResult Role([FromForm] string roleName)
        {
            _manager.CreateAsync(new ApplicationRole()
            {
                Name = roleName
            });
            return RedirectToAction("Role");
        }

        [Route("{area}/{action}/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _manager.FindByIdAsync(id);
            if (role != null)
                await _manager.DeleteAsync(role);
            return RedirectToAction("Role");
        }

        [Route("{area}/{action}/{id}")]
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            ApplicationRole role = await _manager.FindByIdAsync(id);
            List<EmployeesTable> members = new List<EmployeesTable>();
            List<EmployeesTable> nonMembers = new List<EmployeesTable>();
            foreach (EmployeesTable user in _users.Users)
            {
                var list = await _users.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [Route("{area}/{action}/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] RoleModification model)
        {
            var role = await _manager.FindByIdAsync(model.RoleId);
            role.Name = model.RoleName;
            await _manager.UpdateAsync(role);

            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    EmployeesTable user = await _users.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _users.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            return BadRequest(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    EmployeesTable user = await _users.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _users.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            return BadRequest(result);
                    }
                }
            }

            return RedirectToAction("Role");
        }
    }
}
