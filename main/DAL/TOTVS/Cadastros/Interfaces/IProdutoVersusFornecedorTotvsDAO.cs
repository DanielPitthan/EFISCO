using DAL.DAOBase;
using Models.Cadastros.Produtos;
using Models.TOTVS.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.TOTVS.Cadastros.Interfaces
{
    public interface IProdutoVersusFornecedorTotvsDAO: IDataAccessBaseTOTVS
    {
        public IQueryable<ProdutoVersusFornecedorTotvs> All();

    }
}
