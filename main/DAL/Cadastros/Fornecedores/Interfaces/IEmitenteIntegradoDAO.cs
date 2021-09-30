using DAL.DAOBaseNfeXml;
using Models.Cadastros.Fornecedores;
using System.Linq;

namespace DAL.Cadastros.Fornecedores.Interfaces
{
    public interface IEmitenteIntegradoDAO : IDataAccessBaseNfeXml
    {
        public IQueryable<EmitenteIntegrado> All();
    }
}
