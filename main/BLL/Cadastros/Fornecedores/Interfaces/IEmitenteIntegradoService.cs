using Models.Cadastros.Fornecedores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cadastros.Fornecedores.Interfaces
{
    public interface IEmitenteIntegradoService
    {
        public Task<bool> AdicionarAsync(EmitenteIntegrado emitente);
        public Task<bool> AtualizarAsync(EmitenteIntegrado emitente);
        public Task<bool> ExcluirAsync(EmitenteIntegrado emitente);
        public Task<IList<EmitenteIntegrado>> ListarNaoIntegradosAsync();
        public Task<EmitenteIntegrado> Get(int emitId);
       
    }
}
