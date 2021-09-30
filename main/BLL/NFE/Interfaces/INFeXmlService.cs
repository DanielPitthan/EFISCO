using Models.FileStoranges;
using Models.Infra;
using Models.NFe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XmlNFe.Nfes;

namespace BLL.NFE.Interfaces
{
    public interface INFeXmlService
    {
        public double StatusOfCurrentProcess { get; set; }
        public int EndOfProcess { get; set; }

        public Task<NFeFiles> ObterNFEFilesPelaChave(string chaveNfe);
        public Task<IList<NFeFiles>> ListarXMLSNaoProcessados(bool apenasValidos = false, bool apenasAuditado = false);
        public Task<IList<NFeFiles>> ListarXMLSProcessados(bool apenasValidos = false, bool apenasNaoValidados = false);
        public Task<IList<NFeFiles>> ListarTodosXML();
        public IQueryable<NFeFiles> ListarTodosXMLQuery();
        public Task ProcessarArquivos();
        public Task<ExcelResult> ValidarArquivos(IList<FileStorange> fileStoranges);
        public Task<IList<NFe>> ObterNotasByFiles();
        public Task Revalidar(NFeFiles nfeFiles);
        public Task Revalidar(string chaveNFE);
        public NFe GetNfeByChave(string chave);
        public Task<bool> Atualiza(NFe nfe);
    }
}
