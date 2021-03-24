using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SARH___JMéndez_Constructora.Data;
using SARH___JMéndez_Constructora.Models;

namespace SARH___JMéndez_Constructora.Controllers
{
    [Authorize]
    public class UserPageSettingsController : Controller
    {
        private readonly ApplicationDbContext _appContext;
        public UserPageSettingsController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        [HttpGet]
        public void SetTheme(bool LightVersionEnabled)
        { 
            try
            {
                var user = _appContext.Aspnetusersref
                    .SingleOrDefault(u => u.UserName.Equals(User.Identity.Name));
                user.LightVersionEnabled = LightVersionEnabled;
                _appContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public void SetRTL(bool RightToLeftEnabled)
        { 
            try
            {
                var user = _appContext.Aspnetusersref
                    .SingleOrDefault(u => u.UserName.Equals(User.Identity.Name));
                user.RightToLeftEnabled = RightToLeftEnabled;
                _appContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        #region Helpers
        #endregion
    }
}
