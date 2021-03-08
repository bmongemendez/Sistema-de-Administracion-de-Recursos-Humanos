using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //var user = _roleManager.FindByIdAsync(id);
            
            return View(await _roleManager.FindByIdAsync(id));
        }
    }
}
