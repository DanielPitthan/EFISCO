using DAL.DAOBase;
using Models.TOTVS.Cadastros.ClienteFornecedor;
using System.Linq;

namespace DAL.TOTVS.Cadastros.Interfaces
{
    public interface IFornecedorTotvsDAO : IDataAccessBaseTOTVS
    {
        public IQueryable<FornecedorTotvs> All();
        public string GetMaxCod(string filial);
    }
}
