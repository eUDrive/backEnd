using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Certificate;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Core.Certificates
{
    public class CertificateAction
    {
        protected List<CertificateDto> ExecuteGetAllCertificatesAction()
        {
            using (var db = new CertificateContext())
            {
                return db.Certificates
                    .Select(c => new CertificateDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Price = c.Price,
                        Stock = c.Stock,
                        CreatedAt = c.CreatedAt,
                        IsActive = c.IsActive
                    })
                    .ToList();
            }
        }

        protected CertificateDto ExecuteGetCertificateByIdAction(int id)
        {
            if (id <= 0) return null;

            using (var db = new CertificateContext())
            {
                var cert = db.Certificates.FirstOrDefault(c => c.Id == id);
                if (cert == null)
                    return null;

                return new CertificateDto
                {
                    Id = cert.Id,
                    Name = cert.Name,
                    Description = cert.Description,
                    Price = cert.Price,
                    Stock = cert.Stock,
                    CreatedAt = cert.CreatedAt,
                    IsActive = cert.IsActive
                };
            }
        }

        protected ResponseMsg ExecuteCreateCertificateAction(CertificateDto certificate)
        {
            if (certificate == null)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Certificate can't be null" 
                };
            }
                
            if (string.IsNullOrWhiteSpace(certificate.Name))
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Certificate name is required" 
                };
            }
                
            if (certificate.Price <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Price must be greater than 0" 
                };
            }

            using (var db = new CertificateContext())
            {
                var cert = new CertificateData
                {
                    Name = certificate.Name,
                    Description = certificate.Description,
                    Price = certificate.Price,
                    Stock = certificate.Stock,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                };

                db.Certificates.Add(cert);
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Certificate created successfully"
                };
            }
        }

        protected ResponseMsg ExecuteUpdateCertificateAction(CertificateDto certificate)
        {
            if (certificate == null)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Certificate can't be null" 
                };
            }
                
            if (certificate.Id <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Certificate ID is required" 
                };
            }
                
            if (string.IsNullOrWhiteSpace(certificate.Name))
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Certificate name is required" 
                };
            }

            using (var db = new CertificateContext())
            {
                var cert = db.Certificates.FirstOrDefault(c => c.Id == certificate.Id);
                if (cert == null)
                    return new ResponseMsg { IsSuccess = false, Message = "Certificate not found" };

                cert.Name = certificate.Name;
                cert.Description = certificate.Description;
                cert.Price = certificate.Price;
                cert.Stock = certificate.Stock;
                cert.IsActive = certificate.IsActive;

                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Certificate updated successfully"
                };
            }
        }

        protected ResponseMsg ExecuteDeleteCertificateAction(int id)
        {
            if (id <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Certificate ID is required" 
                };
            }

            using (var db = new CertificateContext())
            {
                var cert = db.Certificates.FirstOrDefault(c => c.Id == id);
                if (cert == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Certificate not found"
                    };
                }

                cert.IsActive = false;
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Certificate deleted successfully"
                };
            }
        }
    }
}
