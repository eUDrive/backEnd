using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Interfaces
{
    public interface IProductActions
    {
        List<ProductDto> GetAllProductsAction();
        ProductDto? GetProductByIdAction(int id);
        ResponseAction<ProductDto> CreateProductAction(ProductCreateDto product);
        ResponseMsg UpdateProductAction(ProductDto product);
        ResponseMsg DeleteProductAction(int id);
    }
}
