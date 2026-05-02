using eUDrive.BusinessLogic.Functions.Certificate;
using eUDrive.BusinessLogic.Functions.Orders;
using eUDrive.BusinessLogic.Functions.Products;
using eUDrive.BusinessLogic.Functions.Sessions;
using eUDrive.BusinessLogic.Functions.Users;
using eUDrive.BusinessLogic.Interfaces;

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

        public ISessionActions GetSessionActions()
        {
            return new SessionFlow();
        }

        public IOrderActions GetOrderActions()
        {
            return new OrderFlow();
        }

        public ICertificateActions GetCertificateActions()
        {
            return new CertificateFlow();
        }
    }
}
