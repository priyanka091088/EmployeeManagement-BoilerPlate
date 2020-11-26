using System.Threading.Tasks;
using EmployeeManagement.Models.TokenAuth;
using EmployeeManagement.Web.Controllers;
using Shouldly;
using Xunit;

namespace EmployeeManagement.Web.Tests.Controllers
{
    public class HomeController_Tests: EmployeeManagementWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}