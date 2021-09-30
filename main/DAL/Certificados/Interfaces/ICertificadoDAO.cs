using DAL.DAOBaseNfeXml;
using Microsoft.EntityFrameworkCore;
using Models.Certificados;
using System.Threading.Tasks;

namespace DAL.Certificados.Interfaces
{
    public interface ICertificadoDAO : IDataAccessBaseNfeXml
    {
        public DbSet<Certificado> All();
        public Task<Certificado> GetById(int id);
    }
}
