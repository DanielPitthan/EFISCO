using ContextBinds.EntityCore;
using DAL.Certificados.Interfaces;
using DAL.DAOBaseNfeXml;
using Microsoft.EntityFrameworkCore;
using Models.Certificados;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Certificados.DAO
{
    public class CertificadoDAO : DataAccessBaseNfeXml, ICertificadoDAO
    {

        public CertificadoDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public DbSet<Certificado> All()
        {
            return base.Contexto.Certificado;
        }

        public async Task<Certificado> GetById(int id)
        {
            return await base.Contexto
                                .Certificado
                                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
