using eUDrive.BusinessLogic.Core.Certificates;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Functions.Certificate
{
    public class CertificateFlow : CertificateAction, ICertificateActions
    {
        public List<CertificateDto> GetAllCertificatesAction()
        {
            return ExecuteGetAllCertificatesAction();
        }

        public CertificateDto GetCertificateByIdAction(int id)
        {
            return ExecuteGetCertificateByIdAction(id);
        }

        public ResponseMsg CreateCertificateAction(CertificateDto certificate)
        {
            return ExecuteCreateCertificateAction(certificate);
        }

        public ResponseMsg UpdateCertificateAction(CertificateDto certificate)
        {
            return ExecuteUpdateCertificateAction(certificate);
        }

        public ResponseMsg DeleteCertificateAction(int id)
        {
            return ExecuteDeleteCertificateAction(id);
        }
    }
}
