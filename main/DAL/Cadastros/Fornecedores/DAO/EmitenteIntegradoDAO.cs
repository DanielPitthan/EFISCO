using ContextBinds.EntityCore;
using DAL.Cadastros.Fornecedores.Interfaces;
using DAL.DAOBaseNfeXml;
using Models.Cadastros.Fornecedores;
using System.Linq;

namespace DAL.Cadastros.Fornecedores.DAO
{
    public class EmitenteIntegradoDAO : DataAccessBaseNfeXml, IEmitenteIntegradoDAO
    {
        public EmitenteIntegradoDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public IQueryable<EmitenteIntegrado> All()
        {
            return Contexto.EmitenteIntegrado;
        }
    }
}
