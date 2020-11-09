using Models.Cadastros.Fornecedores;
using Models.TOTVS.Cadastros.ClienteFornecedor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TOTVS.Cadastros.Interfaces
{
    public interface IFornecedorTotvsService
    {
        Task<FornecedorTotvs> LocateByCnpj(string filial, string cnpj);
        Task<bool> Cadastrar(FornecedorTotvs fornecedor, int emitId);
    }
}
