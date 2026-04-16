using eUDrive.BusinessLogic.Core;
using eUDrive.BusinessLogic.Functions.Products;
using eUDrive.BusinessLogic.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic
{
    public class BusinessLogic
    {
        public IProductActions GetProductActions()
        {
            return new ProductFlow();
        }
    }
}