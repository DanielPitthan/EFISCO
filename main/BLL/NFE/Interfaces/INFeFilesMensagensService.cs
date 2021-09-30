using Models.NFe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.NFE.Interfaces
{
    public interface INFeFilesMensagensService
    {
        Task<bool> Adcionar(NFeFilesMensagens nFeFilesMensagens);
        Task<bool> Excluir(NFeFilesMensagens nFeFilesMensagens);
        Task<bool> Alterar(NFeFilesMensagens nFeFilesMensagens);
        Task<IList<NFeFilesMensagens>> ListarErroDoArquivoAsync(NFeFiles nfeFile);
        IList<NFeFilesMensagens> ListarErroDoArquivo(NFeFiles nfeFile);

        /// <summary>
        /// Lista erros pela Chave
        /// </summary>
        /// <param name="chaveNFE"></param>
        /// <returns></returns>
        Task<IList<NFeFilesMensagens>> ListarErroDoArquivoAsync(string chaveNFE);
    }
}
