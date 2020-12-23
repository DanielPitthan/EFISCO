using DAL.DAOBase;
using Models.TOTVS.Cadastros.ClienteFornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.TOTVS.Cadastros.Interfaces
{
    public interface IFornecedorTotvsDAO:IDataAccessBaseTOTVS
    {
        public IQueryable<FornecedorTotvs> All();
        public string GetMaxCod(string filial);
    }
}
