using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Controllers
{
    public abstract class EmployeeManagementControllerBase: AbpController
    {
        protected EmployeeManagementControllerBase()
        {
            LocalizationSourceName = EmployeeManagementConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
