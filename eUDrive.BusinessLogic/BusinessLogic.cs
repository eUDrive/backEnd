using eUDrive.BusinessLogic.Functions.Users;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.BusinessLogic.Functions.Products;
using eUDrive.BusinessLogic.Functions.Orders;
using eUDrive.BusinessLogic.Functions.Sessions;

namespace eUDrive.BusinessLogic
{
    public class BusinessLogic
    {
        public IProductActions GetProductActions()
        {
            return new ProductFlow();
        }

        public IUserActions GetUserActions()
        {
            return new UserFlow();
        }

        public IOrderActions GetOrderActions()
        {
            return new OrderFlow();
        }

        public ISessionActions GetSessionActions()
        {
            return new SessionFlow();
        }
    }
}
