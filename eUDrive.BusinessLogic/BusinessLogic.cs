using eUDrive.BusinessLogic.Interfaces;

namespace eUDrive.BusinessLogic
{
    public class BusinessLogic
    {
        public IAuthActions GetAuthActions()
        {
            return new AuthFlow();
        }
    }
}
