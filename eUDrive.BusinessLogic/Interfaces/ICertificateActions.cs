using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Interfaces
{
    public interface ICertificateActions
    {
        List<CertificateDto> GetAllCertificatesAction();
        CertificateDto GetCertificateByIdAction(int id);
        ResponseMsg CreateCertificateAction(CertificateDto certificate);
        ResponseMsg UpdateCertificateAction(CertificateDto certificate);
        ResponseMsg DeleteCertificateAction(int id);
    }
}
