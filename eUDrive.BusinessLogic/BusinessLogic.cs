using eUDrive.BusinessLogic.Functions.Products;
using eUDrive.BusinessLogic.Functions.Users;
using eUDrive.BusinessLogic.Interfaces;

namespace eUDrive.BusinessLogic
{
    public class BusinessLogic
    {
        public IUserActions GetUserActions()
        {
            return new UserFlow();
        }
    }
}
