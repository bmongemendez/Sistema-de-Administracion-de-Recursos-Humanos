using System.Linq;
using SARH___JMéndez_Constructora.Data;

namespace SARH___JMéndez_Constructora.Models
{
    public class UserPageSettingsService
    {
        private readonly ApplicationDbContext _appContext;
        public UserPageSettingsService(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        
        public Aspnetusersref GetUserPageSettings(string userName)
        {
            return _appContext.Aspnetusersref
                .SingleOrDefault(u => u.UserName.Equals(userName));
        }
        #region Helpers
        #endregion
    }
}