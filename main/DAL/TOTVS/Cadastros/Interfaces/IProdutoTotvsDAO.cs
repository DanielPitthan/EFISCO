using DAL.DAOBase;
using Models.TOTVS.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.TOTVS.Cadastros.Interfaces
{
    public interface IProdutoTotvsDAO:IDataAccessBaseTOTVS
    {
        public IQueryable<ProdutoTotvs> All();
    }
}
