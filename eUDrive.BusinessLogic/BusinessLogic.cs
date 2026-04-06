using eUDrive.BusinessLogic.Core;
using eUDrive.BusinessLogic.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {

    }

    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}