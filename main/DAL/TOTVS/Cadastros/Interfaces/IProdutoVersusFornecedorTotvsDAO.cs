using DAL.DAOBase;
using Models.TOTVS.Cadastros;
using System.Linq;

namespace DAL.TOTVS.Cadastros.Interfaces
{
    public interface IProdutoVersusFornecedorTotvsDAO : IDataAccessBaseTOTVS
    {
        public IQueryable<ProdutoVersusFornecedorTotvs> All();

    }
}
