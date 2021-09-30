using DAL.DAOBaseNfeXml;
using Models.Cadastros.Produtos;
using System.Linq;

namespace DAL.Cadastros.Produtos.Interfaces
{
    public interface IProdutoIntegradoDAO : IDataAccessBaseNfeXml
    {
        IQueryable<ProdutoIntegrado> All();
    }
}
