using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace EmployeeManagement.Authorization
{
    public class EmployeeManagementAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Departments, L("Departments"));
            context.CreatePermission(PermissionNames.Pages_Employee, L("Employees"));
            context.CreatePermission(PermissionNames.Pages_EmployeeProfile, L("Employee Profile"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, EmployeeManagementConsts.LocalizationSourceName);
        }
    }
}
