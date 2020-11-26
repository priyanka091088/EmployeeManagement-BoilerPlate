using Abp.Authorization;
using EmployeeManagement.Authorization.Roles;
using EmployeeManagement.Authorization.Users;

namespace EmployeeManagement.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
