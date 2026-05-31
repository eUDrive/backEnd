using eUDrive.BusinessLogic.Functions.Certificate;
using eUDrive.BusinessLogic.Functions.Orders;
using eUDrive.BusinessLogic.Functions.Products;
using eUDrive.BusinessLogic.Functions.Users;
using eUDrive.BusinessLogic.Functions.Category;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.BusinessLogic.Functions.Images;

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

        public ICertificateActions GetCertificateActions()
        {
            return new CertificateFlow();
        }

        public ICategoryActions GetCategoryActions() 
        {
            return new CategoryFlow();
        }

        public IImageActions GetImageActions()
        {
            return new ImageFlow();
        }
    }
}
