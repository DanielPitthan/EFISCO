using BLL.Certificados.Interfaces;
using CrossCuting.Tools;
using DAL.Certificados.Interfaces;
using DAL.FileStoranges.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Certificados;
using Models.Empresas;
using Models.FileStoranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BLL.Certificados.Services
{
    public class CertificadoService : ICertificadoService
    {
        private readonly ICertificadoDAO certificadoDAO;
        private readonly IFileStorangeDAO fileStorangeDAO;


        public CertificadoService(ICertificadoDAO _certificadoDAO,
                                IFileStorangeDAO _fileStorangeDAO)
        {
            certificadoDAO = _certificadoDAO;
            fileStorangeDAO = _fileStorangeDAO;
        }

        public async Task<bool> AdcionarByFileStorange(FileStorange fileStorange, string senha, string cnpj, int empresaId)
        {
            SecureString password = TransformToSecureString.Transform(senha);
            try
            {
                FileStorange file = fileStorangeDAO.GetAll()
                                                .Where(x => x.Id == fileStorange.Id)
                                                .FirstOrDefault();



                X509Certificate2 certificade = new X509Certificate2(file.DataByte, password, X509KeyStorageFlags.MachineKeySet);


                Certificado cert = new Certificado
                {
                    Cnpj = cnpj,
                    Certf = file.DataByte,
                    Ativo = true,
                    Senha = senha,
                    DataInclusao = DateTime.Now,
                    DataExpiracao = Convert.ToDateTime(certificade.GetExpirationDateString()),
                    Empresa = new Empresa
                    {
                        Id = empresaId
                    }
                };

                bool result = await certificadoDAO.AddSysnc(cert);
                if (result)
                {
                    file.Processado = true;
                    await fileStorangeDAO.UpdateAsync(file);
                }


                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<bool> Alterar(Certificado cert)
        {
            bool result = await certificadoDAO.UpdateAsync(cert);
            return result;
        }

        public async Task<Certificado> GetByCnpj(string cnpj)
        {
            Certificado cert = await certificadoDAO.All()
                .Where(x => x.Cnpj == cnpj)
                .FirstOrDefaultAsync();
            return cert;
        }

        public async Task<Certificado> GetById(int id)
        {
            return await certificadoDAO.GetById(id);
        }

        public async Task<IList<Certificado>> List()
        {
            List<Certificado> list = await certificadoDAO.All()
                                 .Include(x => x.Empresa)
                                 .OrderByDescending(x => x.Ativo)
                                 .ThenBy(x => x.DataExpiracao)
                                 .ToListAsync();
            return list;
        }


    }
}
