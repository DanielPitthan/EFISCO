using Models.Certificados;
using Models.FileStoranges;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Certificados.Interfaces
{
    public interface ICertificadoService
    {
        Task<Certificado> GetById(int id);
        Task<Certificado> GetByCnpj(string cnpj);
        Task<IList<Certificado>> List();
        Task<bool> AdcionarByFileStorange(FileStorange fileStorange, string senha, string cnpj, int empresaId);
        Task<bool> Alterar(Certificado cert);
    }
}
