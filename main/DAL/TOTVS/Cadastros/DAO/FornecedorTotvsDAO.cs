using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.TOTVS.Cadastros.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.TOTVS.Cadastros.ClienteFornecedor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            var query = FornecedorQuery.Insert(fornecedor);
          
            var fornecedorInserido = await this.Contexto.SA2010
                                                        .FromSqlRaw(query)
                                                        .ToListAsync();
            return fornecedorInserido != null;

        }

        public IQueryable<FornecedorTotvs> All()
        {
            return this.Contexto.SA2010;
        }

        public string GetMaxCod(string filial)
        {
            var MaxCode = this.Contexto.SA2010
                                         .FromSqlRaw(FornecedorQuery.MaxCode(filial))
                                         .FirstOrDefault();
            return MaxCode.A2_COD;
        }
    }
}
