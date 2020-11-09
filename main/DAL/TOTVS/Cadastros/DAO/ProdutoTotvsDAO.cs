using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.TOTVS.Cadastros.Interfaces;
using Models.TOTVS.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.TOTVS.Cadastros.DAO
{
    public class ProdutoTotvsDAO : DataAccessBaseTOTVS, IProdutoTotvsDAO
    {
        public ProdutoTotvsDAO(ContextTOTVS _context) : base(_context)
        {
        }

        public override Task<bool> AddRawSql<TSource>(TSource item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProdutoTotvs> All()
        {
            return this.Contexto.SB1010;
        }
    }
}
