using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.TOTVS.Cadastros.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.TOTVS.Cadastros.ClienteFornecedor;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.TOTVS.Cadastros.DAO
{
    public class FornecedorTotvsDAO : DataAccessBaseTOTVS, IFornecedorTotvsDAO
    {
        public FornecedorTotvsDAO(ContextTOTVS _context) : base(_context)
        {
        }

        public override async Task<bool> AddRawSql<TSource>(TSource item)
        {
            FornecedorTotvs fornecedor = item as FornecedorTotvs;
            string query = FornecedorQuery.Insert(fornecedor);

            int fornecedorInserido = await Contexto.Database.ExecuteSqlRawAsync(query);


            return fornecedorInserido > 0;

        }

        public IQueryable<FornecedorTotvs> All()
        {
            return Contexto.SA2010;
        }

        public string GetMaxCod(string filial)
        {
            FornecedorTotvs MaxCode = Contexto.SA2010
                                         .FromSqlRaw(FornecedorQuery.MaxCode(filial))
                                         .FirstOrDefault();
            return MaxCode.A2_COD;
        }
    }
}
