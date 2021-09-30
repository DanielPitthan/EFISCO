using Models.TOTVS.Cadastros.Produtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.TOTVS.Cadastros.Interfaces
{
    public interface IProdutoTotvsService
    {
        Task<ProdutoTotvs> GetByRef(string filial, string referencia);
        Task<IList<ProdutoTotvs>> GetAllByNCM(string filial, string ncm);
    }
}
