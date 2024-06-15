using Diplomm.Areas.Account.Models;
using Diplomm.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Diplomm.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "u-role")]
    public class RoleTagHelper : TagHelper
    {
        private readonly UserManager<EmployeesTable> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        [HtmlAttributeName("u-role")]
        public string Role { get; set; }

        public RoleTagHelper(UserManager<EmployeesTable> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string?> users = [];
            ApplicationRole role = await roleManager.FindByIdAsync(Role);
            if (role != null)
            {
                users = (await userManager.GetUsersInRoleAsync(role.Name))
                    .Select((user) => user.UserName)
                    .ToList();
            }
            output.Content.SetContent(users.Count == 0 ? "Нет пользователей" : string.Join(", ", users));
        }
    }
}