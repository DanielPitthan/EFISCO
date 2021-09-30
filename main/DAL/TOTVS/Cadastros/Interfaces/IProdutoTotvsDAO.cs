using DAL.DAOBase;
using Models.TOTVS.Cadastros.Produtos;
using System.Linq;

namespace DAL.TOTVS.Cadastros.Interfaces
{
    public interface IProdutoTotvsDAO : IDataAccessBaseTOTVS
    {
        public IQueryable<ProdutoTotvs> All();
    }
}
