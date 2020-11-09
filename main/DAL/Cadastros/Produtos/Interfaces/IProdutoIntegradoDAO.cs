using DAL.DAOBaseNfeXml;
using Models.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Cadastros.Produtos.Interfaces
{
    public interface IProdutoIntegradoDAO: IDataAccessBaseNfeXml
    {
        IQueryable<ProdutoIntegrado> All();
    }
}
