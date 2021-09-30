using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.TOTVS.Cadastros.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Cadastros.Produtos;
using Models.TOTVS.Cadastros;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.TOTVS.Cadastros.DAO
{
    public class ProdutoVersusFornecedorTotvsDAO : DataAccessBaseTOTVS, IProdutoVersusFornecedorTotvsDAO
    {
        public ProdutoVersusFornecedorTotvsDAO(ContextTOTVS _context) : base(_context)
        {
        }

        public override async Task<bool> AddRawSql<TSource>(TSource item)
        {

            if (!(item is ProdutoIntegrado))
            {
                return false;
            }

            ProdutoIntegrado produtoIntegrado = item as ProdutoIntegrado;
            Contexto.SA5010.FromSqlRaw(ProdutoVsFornecedorQuery.Insert(produtoIntegrado))
                .AsEnumerable()
                .FirstOrDefault();
            return true;

        }

        public IQueryable<ProdutoVersusFornecedorTotvs> All()
        {
            return Contexto.SA5010;
        }
    }
}
