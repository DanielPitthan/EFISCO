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
       public Task<FornecedorTotvs> LocateByCnpjAsync(string filial, string cnpj);
       public FornecedorTotvs LocateByCnpj(string filial, string cnpj);
       public Task<bool> Cadastrar(FornecedorTotvs fornecedor, int emitId);
    }
}
