
using BLL.Cadastros.Fornecedores.Interfaces;
using BLL.TOTVS.Cadastros.Interfaces;
using ConsoleApp1;
using DAL.TOTVS.Cadastros.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Cadastros.Fornecedores;
using Models.TOTVS.Cadastros.ClienteFornecedor;
using System.Linq;
using System.Threading.Tasks;
using XmlNFe.Nfes.Informacoes.Emitente;

namespace BLL.TOTVS.Cadastros.Servicos
{
    public class FornecedorTotvsService : IFornecedorTotvsService
    {
        private IFornecedorTotvsDAO fornecedorTotvsDAO;
        private IEmitenteIntegradoService emitenteService;

        public FornecedorTotvsService(IFornecedorTotvsDAO _fornecedorTotvs,
                                        IEmitenteIntegradoService emitenteIntegradoService)
        {
            this.fornecedorTotvsDAO = _fornecedorTotvs;
            this.emitenteService = emitenteIntegradoService;
        }

        public async Task<bool> Cadastrar(FornecedorTotvs fornecedor, int emitId)
        {
            //var recno = this.fornecedorTotvsDAO.All().Max(r => r.R_E_C_N_O_);
         
            EmitenteIntegrado Emitente = await emitenteService.Get(emitId);
            // var codigo = this.fornecedorTotvsDAO.GetMaxCod(Emitente.CodigoTotvsEmpresaFilial.Substring(0, 2));

            //fornecedor.R_E_C_N_O_ = recno + 1;
            // fornecedor.A2_COD = codigo;//Soma1.GetNextNum(codigo);
            fornecedor.A2_MUN = fornecedor.A2_MUN.Replace("'", "");
            fornecedor.A2_LOJA = "01";
            fornecedor.A2_CODPAIS = "01058";// BRASIL
            fornecedor.A2_PAIS = "105";// BRASIL
            fornecedor.A2_TIPO = "J";
            fornecedor.A2_FILIAL = Emitente.CodigoTotvsEmpresaFilial.Substring(0,2);

            //Grava o fornecedor
            var success = await this.fornecedorTotvsDAO.AddRawSql(fornecedor);

            if (success)
            {               
                Emitente.IntegradaoTOTVS = true;
                success = await emitenteService.AtualizarAsync(Emitente);
            }

            return success;
        }

        public async Task<FornecedorTotvs> LocateByCnpjAsync(string filial, string cnpj)
        {
            FornecedorTotvs fornecedor = await this.fornecedorTotvsDAO.All()
                .Where(x => x.A2_CGC == cnpj )
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return fornecedor;
        }
        public FornecedorTotvs LocateByCnpj(string filial, string cnpj)
        {
            FornecedorTotvs fornecedor = this.fornecedorTotvsDAO.All()
                .Where(x => x.A2_CGC == cnpj )
                .AsNoTracking()
                .FirstOrDefault();
            return fornecedor;
        }

    }


}
