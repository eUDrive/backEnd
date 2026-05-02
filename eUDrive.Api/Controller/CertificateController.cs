using eUDrive.Api.Filters;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Certificate;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/certificate")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private ICertificateActions _certificate;

        public CertificateController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _certificate = bl.GetCertificateActions();
        }

        [HttpGet("getAll")]
        public IActionResult GetAllCertificates()
        {
            var certificates = _certificate.GetAllCertificatesAction();
            return Ok(certificates);
        }

        [HttpGet("id")]
        public IActionResult GetCertificateById(int id)
        {
            var certificate = _certificate.GetCertificateByIdAction(id);
            if (certificate == null)
                return NotFound(new { message = "Certificate not found" });
            return Ok(certificate);
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult CreateCertificate([FromBody] CertificateDto certificate)
        {
            var result = _certificate.CreateCertificateAction(certificate);
            return Ok(result);
        }

        [HttpPut]
        [AdminOnly]
        public IActionResult UpdateCertificate([FromBody] CertificateDto certificate)
        {
            var result = _certificate.UpdateCertificateAction(certificate);
            return Ok(result);
        }

        [HttpDelete("id")]
        [AdminOnly]
        public IActionResult DeleteCertificate(int id)
        {
            var result = _certificate.DeleteCertificateAction(id);
            return Ok(result);
        }
    }
}
