using eUDrive.BusinessLogic.Core.Products;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Functions.Products
{
    public class ProductFlow : ProductAction, IProductActions
    {
        public List<ProductDto> GetAllProductsAction()
        {
            return ExecuteGetAllProductsAction();
        }

        public ProductDto? GetProductByIdAction(int id)
        {
            return ExecuteGetProductByIdAction(id);
        }

        public ResponseAction<ProductDto> CreateProductAction(ProductCreateDto product)
        {
            return ExecuteCreateProductAction(product);
        }

        public ResponseMsg UpdateProductAction(int id, UpdateProductDto product)
        {
            return ExecuteUpdateProductAction(id, product);
        }

        public ResponseMsg DeleteProductAction(int id)
        {
            return ExecuteDeleteProductAction(id);
        }
    }
}
