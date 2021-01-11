using BLL.Cadastros.Fornecedores.Interfaces;
using BLL.Cadastros.Produtos.Interfaces;
using BLL.Empresas.Interfaces;
using BLL.NFE.Interfaces;
using BLL.TOTVS.Cadastros.Interfaces;
using BLL.TOTVS.Movimentos.Compras.Interfaces;
using DAL.FileStoranges.Interfaces;
using DAL.XmlDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Cadastros.Fornecedores;
using Models.Cadastros.Produtos;
using Models.FileStoranges;
using Models.Infra;
using Models.NFe;
using Models.TOTVS.Cadastros;
using Models.TOTVS.Cadastros.ClienteFornecedor;
using Models.TOTVS.Cadastros.Produtos;
using Models.TOTVS.Movimentos;
using Models.TOTVS.Movimentos.Compras;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XmlNFe.Nfes;
using XmlNFe.Nfes.Informacoes.Detalhe;

namespace BLL.NFE.Services
{
    public class NFeXmlService : INFeXmlService
    {
        private readonly INFeDAO nFeDAO;
        private readonly INFeFilesDAO nFeFilesDAO;
        private readonly IFileStorangeDAO fileStorangeDAO;
        private readonly IEmpresaService empresaService;
        private readonly IFornecedorTotvsService fornecedorService;
        private readonly IProdutoTotvsService produtoTotvsService;
        private readonly INFeFilesMensagensService nFeFilesMensagensService;
        private readonly IProdutoVersusFornecedorTotvsService produtoVersusFornecedorTotvsService;
        private readonly IPedidoDeCompraTotvsService pedidoTotvsService;
        private readonly INotaFiscalEntradaTotvsService notaFiscalEntradaTotvsService;
        private readonly IEmitenteIntegradoService emitenteService;
        private readonly IProdutoIntegradoService produtoIntegradoService;

        public double StatusOfCurrentProcess { get; set; }
        public int EndOfProcess { get; set; }

        public NFeXmlService(INFeDAO _nFeDAO,
                             INFeFilesDAO _nFeFilesDAO,
                             IFileStorangeDAO _fileStorangeDAO,
                             IEmpresaService _empresaService,
                             IFornecedorTotvsService _fornecedorTotvsService,
                             IProdutoTotvsService _produtoTotvsService,
                             INFeFilesMensagensService _nFeFilesMensagensService,
                             IProdutoVersusFornecedorTotvsService _produtoVersusFornecedorTotvsService,
                             IPedidoDeCompraTotvsService _pedidoTotvsService,
                             INotaFiscalEntradaTotvsService _notaFiscalEntradaTotvsService,
                             IEmitenteIntegradoService _emitenteService,
                             IProdutoIntegradoService _produtoIntegradoService)
        {
            this.nFeDAO = _nFeDAO;
            this.nFeFilesDAO = _nFeFilesDAO;
            this.fileStorangeDAO = _fileStorangeDAO;
            this.empresaService = _empresaService;
            this.fornecedorService = _fornecedorTotvsService;
            this.produtoTotvsService = _produtoTotvsService;
            this.nFeFilesMensagensService = _nFeFilesMensagensService;
            this.produtoVersusFornecedorTotvsService = _produtoVersusFornecedorTotvsService;
            this.pedidoTotvsService = _pedidoTotvsService;
            this.notaFiscalEntradaTotvsService = _notaFiscalEntradaTotvsService;
            this.emitenteService = _emitenteService;
            this.produtoIntegradoService = _produtoIntegradoService;
        }



        public async Task<IList<NFeFiles>> ListarXMLSNaoProcessados(bool apenasValidos = false, bool apenasAuditado = false)
        {
            IQueryable<NFeFiles> query = this.nFeFilesDAO.GetAll();

            query = query.Where(nf => nf.Processada == false);

            if (apenasValidos)
            {
                query = query.Where(x => x.AutoValidado == apenasValidos);
            }
            else
            {
                query = query.Where(x => x.AutoValidado == false);
            }

            if (apenasAuditado)
            {
                query = query.Where(x => x.Validado == apenasAuditado);
            }


            query = query.OrderBy(x => x.Processada)
                             .ThenBy(x => x.DataEmnissaoNfe);

            IList<NFeFiles> arquivos = await query.ToListAsync();

            return arquivos;
        }
        public async Task<IList<NFeFiles>> ListarXMLSProcessados(bool apenasValidos = false, bool apenasNaoValidados = false)
        {
            IQueryable<NFeFiles> query = this.nFeFilesDAO.GetAll();

            if (apenasValidos)
                query = query.Where(x => x.Validado == apenasValidos);

            if (apenasNaoValidados)
                query = query.Where(x => x.Validado == !apenasNaoValidados);

            query = query.Where(x => x.Processada == true);

            query = query.OrderBy(x => x.Processada)
                             .ThenBy(x => x.DataEmnissaoNfe);

            IList<NFeFiles> arquivos = await query.ToListAsync();

            return arquivos;
        }

        public async Task<IList<NFeFiles>> ListarTodosXML(bool apenasValidos = false, bool apenasNaoValidados = false)
        {
            IQueryable<NFeFiles> query = this.nFeFilesDAO.GetAll();



            query = query.OrderBy(x => x.Processada)
                             .ThenBy(x => x.DataEmnissaoNfe);

            IList<NFeFiles> arquivos = await query.ToListAsync();

            return arquivos;
        }

        public async Task ProcessarArquivos()
        {


            var arquivosAProcessar = await fileStorangeDAO.GetAll()
                                     .Where(f => f.Processado == false && f.FileType == ".xml")
                                     .ToListAsync();


            EndOfProcess = arquivosAProcessar.Count;
            StatusOfCurrentProcess = 0;
            double contagem = 0;

            foreach (var arquivo in arquivosAProcessar)
            {

                #region Faz todos os loads
                List<NFeFilesMensagens> mensagensDeErro = new List<NFeFilesMensagens>();
                XmlDocument documento = new XmlDocument();
                documento.LoadXml(arquivo.XmlString);




                NFe notaFiscalLidaXML = await this.nFeDAO.CarregarXML(documento.GetElementsByTagName("NFe")[0].OuterXml);

                NFe notaImportada = await this.nFeDAO.GetAll()
                                                .Where(x => x.infNFe.Id == notaFiscalLidaXML.infNFe.Id)//Chave da NF
                                                .SingleOrDefaultAsync();




                NFeFiles arquivoJaExiste = await this.nFeFilesDAO
                                                 .GetAll()
                                                 .Where(x => x.ChaveAcesso == notaFiscalLidaXML.infNFe.Id)
                                                 .SingleOrDefaultAsync();

                #endregion



                string[] pathSplited = arquivo.Path.Split('\\');
                NFeFiles nfeFiles = new NFeFiles();

                nfeFiles.Id = arquivoJaExiste == null ? 0 : arquivoJaExiste.Id;
                nfeFiles.Arquivo = pathSplited[pathSplited.Length - 1];
                nfeFiles.Path = arquivo.Path;
                nfeFiles.ChaveAcesso = notaFiscalLidaXML.infNFe.Id;
                nfeFiles.CnpjFornecedor = notaFiscalLidaXML.infNFe.emit.CNPJ;
                nfeFiles.DataEmnissaoNfe = notaFiscalLidaXML.infNFe.ide.dhEmi.Value.DateTime;
                nfeFiles.Fornecedor = notaFiscalLidaXML.infNFe.emit.xNome;
                nfeFiles.NumeroNota = notaFiscalLidaXML.infNFe.ide.nNF.ToString().PadLeft(9, '0');
                nfeFiles.Serie = notaFiscalLidaXML.infNFe.ide.serie;
                nfeFiles.ValorTotal = notaFiscalLidaXML.infNFe.total.ICMSTot.vNF;
                nfeFiles.DataInclusao = DateTime.Now;
                nfeFiles.Empresa = await empresaService.GetByCnpjsync(notaFiscalLidaXML.infNFe.dest.CNPJ);
                nfeFiles.AutoValidado = false;
                nfeFiles.Validado = false;




                //Importa o NF-e
                if (notaImportada != null)
                {
                    notaFiscalLidaXML.Id = notaImportada.Id;
                    await this.nFeDAO.UpdateAsync(notaFiscalLidaXML);
                }
                else
                {
                    await this.nFeDAO.AddAsync(notaFiscalLidaXML);
                    
                    //Após a inclusão preciso recuperar a nota para a validações
                    notaImportada = await this.nFeDAO.GetAll()
                                                .Where(x => x.infNFe.Id == notaFiscalLidaXML.infNFe.Id)//Chave da NF
                                                .SingleOrDefaultAsync();
                }



                #region Validações
                //Validação após gravar  Nf-e               

                mensagensDeErro.AddRange(await ValidaNFE(notaImportada, nfeFiles));
                nfeFiles.AutoValidado = !(mensagensDeErro.Count > 0);

                #endregion


                //Atualiza ou insere NFeFile
                if (arquivoJaExiste != null)
                {
                    this.nFeFilesDAO.Update(nfeFiles);
                }
                else
                {
                    await this.nFeFilesDAO.AddSysnc(nfeFiles);
                }

                //Atualiza o arquivo processado 
                arquivo.Processado = true;
                await this.fileStorangeDAO.UpdateAsync(arquivo);

                //Grava as mensagens de erro 
                foreach (var mensagem in mensagensDeErro)
                {
                    await nFeFilesMensagensService.Adcionar(mensagem).ConfigureAwait(false);
                }
                contagem++;
                StatusOfCurrentProcess = (contagem * 100) / arquivosAProcessar.Count;

            }

            StatusOfCurrentProcess = 100;
        }



        private async Task<List<NFeFilesMensagens>> ValidaNFE(NFe notaXml, NFeFiles nfeFiles)
        {
            List<NFeFilesMensagens> mensagensIntegracao = new List<NFeFilesMensagens>();

            try
            {
                mensagensIntegracao.AddRange(await ValidaNotaJaLancada(notaXml, nfeFiles));

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ValidaNotaJaLancada {ex.Message}");
            }

            try
            {
                mensagensIntegracao.AddRange(await ValidaFornecedor(nfeFiles, notaXml));
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ValidaFornecedor {ex.Message}");
            }

            try
            {
                if (mensagensIntegracao.Count == 0)
                    mensagensIntegracao.AddRange(await ValidaProduto(notaXml, nfeFiles));
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ValidaProduto {ex.Message}");
            }

            try
            {
                // Sem Tem xPed ?
                if (notaXml.infNFe.det.Any(x => !string.IsNullOrEmpty(x.prod.xPed)))
                {
                    mensagensIntegracao.AddRange(await ValidaPedido(notaXml, nfeFiles));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ValidaPedido {ex.Message}");
            }






            return mensagensIntegracao;
        }

        /// <summary>
        /// Valida se NF já foi lançada no TOTVS Protheus
        /// </summary>
        /// <param name="notaFiscalLidaXML"></param>
        /// <param name="nfeFiles"></param>
        /// <returns></returns>
        private async Task<IList<NFeFilesMensagens>> ValidaNotaJaLancada(NFe notaFiscalLidaXML, NFeFiles nfeFiles)
        {
            List<NFeFilesMensagens> mensagensDeErro = new List<NFeFilesMensagens>();

            NotaFiscalEntradaCabecalhoTotvs notaFicalEntrada = await this.notaFiscalEntradaTotvsService.GetByChave(notaFiscalLidaXML.infNFe.Id);
            //Se ja existe no TOTVS grava erro
            if (notaFicalEntrada != null)
            {
                mensagensDeErro.Add(new NFeFilesMensagens()
                {
                    NFeFiles = nfeFiles,
                    Ativo = true,
                    DataInclusao = DateTime.Now,
                    ChaveNFe = nfeFiles.ChaveAcesso,
                    Texto = $"Chave já importada no Protheus. Chave: {notaFicalEntrada.F1_CHVNFE} Data Digitação: {notaFicalEntrada.F1_DTDIGIT}, Nota Fiscal: {notaFicalEntrada.F1_SERIE}-{notaFicalEntrada.F1_DOC}"
                });
            }
            return mensagensDeErro;
        }



        /// <summary>
        /// Validação do Pedido do Compras
        /// </summary>
        /// <param name="notaXml"></param>
        /// <param name="nfeFiles"></param>
        /// <returns></returns>
        private async Task<IList<NFeFilesMensagens>> ValidaPedido(NFe notaXml, NFeFiles nfeFiles)
        {
            IList<NFeFilesMensagens> mensagens = new List<NFeFilesMensagens>();
            foreach (var detalhe in notaXml.infNFe.det)
            {
                if (string.IsNullOrEmpty(detalhe.prod.xPed))
                {
                    mensagens.Add(new NFeFilesMensagens()
                    {
                        NFeFiles = nfeFiles,
                        Ativo = true,
                        DataInclusao = DateTime.Now,
                        Texto = $"Item sem pedido de compras {detalhe.prod.cProd} - {detalhe.prod.xProd} ",
                        ChaveNFe = nfeFiles.ChaveAcesso
                    });
                    continue;
                }

                IList<PedidoDeCompraTotvs> pedidos = await this.pedidoTotvsService.GetByPedido(detalhe.prod.xPed);
                if (pedidos == null)
                {
                    mensagens.Add(new NFeFilesMensagens()
                    {
                        NFeFiles = nfeFiles,
                        Ativo = true,
                        DataInclusao = DateTime.Now,
                        Texto = $"Pedido de compras {detalhe.prod.xPed} não localizado na base do Protheus",
                        ChaveNFe = nfeFiles.ChaveAcesso
                    });
                    continue;
                }

                ProdutoVersusFornecedorTotvs produtoFornecedor = await produtoVersusFornecedorTotvsService.Get(nfeFiles.Empresa.CodigoTotvsEmpresaFilial,
                                                                                                                 nfeFiles.CnpjFornecedor,
                                                                                                                 detalhe.prod.cProd).ConfigureAwait(false);
                if (produtoFornecedor != null)
                {
                    PedidoDeCompraTotvs pedido = pedidos
                       .Where(x => x.C7_PRODUTO.Equals(produtoFornecedor.A5_PRODUTO))
                       .SingleOrDefault();

                    if (pedido == null)
                    {
                        mensagens.Add(new NFeFilesMensagens()
                        {
                            NFeFiles = nfeFiles,
                            Ativo = true,
                            DataInclusao = DateTime.Now,
                            Texto = $"Item {produtoFornecedor.A5_PRODUTO} não localizado no pedido de compras. Pedido: {detalhe.prod.xPed}",
                            ChaveNFe = nfeFiles.ChaveAcesso
                        });
                        continue;
                    }

                    double minValue = (double)detalhe.prod.vUnCom - 0.01;
                    double maxValue = (double)detalhe.prod.vUnCom + 0.01;

                    if (!(pedido.C7_PRECO >= minValue && pedido.C7_PRECO <= maxValue))
                    {
                        mensagens.Add(new NFeFilesMensagens()
                        {
                            NFeFiles = nfeFiles,
                            Ativo = true,
                            DataInclusao = DateTime.Now,
                            Texto = $"Item {produtoFornecedor.A5_PRODUTO} com preço divergente do pedido de compras. Pedido: {detalhe.prod.xPed} Unitário no pedido R$ {pedido.C7_PRECO} Unitário na NF {detalhe.prod.vUnCom}",
                            ChaveNFe = nfeFiles.ChaveAcesso
                        });
                        continue;
                    }


                }

            }
            return mensagens;
        }



        /// <summary>
        /// Validações do Produto 
        /// </summary>
        /// <param name="notaXml"></param>
        /// <param name="nfeFiles"></param>
        /// <returns></returns>
        private async Task<List<NFeFilesMensagens>> ValidaProduto(NFe notaXml, NFeFiles nfeFiles)
        {
            List<NFeFilesMensagens> mensagensIntegracao = new List<NFeFilesMensagens>();

            IList<prod> prods;

            foreach (var produto in notaXml.infNFe.det)
            {
                IList<ProdutoTotvs> produtos = await this.produtoTotvsService
                                                         .GetAllByNCM(nfeFiles.Empresa.CodigoTotvsEmpresaFilial, produto.prod.NCM);

                FornecedorTotvs fornecedor = await this.fornecedorService.LocateByCnpjAsync(nfeFiles
                                                                                        .Empresa.CodigoTotvsEmpresaFilial.Substring(0, 2),
                                                                                        nfeFiles.CnpjFornecedor);

                ProdutoVersusFornecedorTotvs produtoVsFornecedor = await produtoVersusFornecedorTotvsService.Get(nfeFiles.Empresa.CodigoTotvsEmpresaFilial,
                                                                                                                 fornecedor.A2_COD,
                                                                                                                 produto.prod.cProd).ConfigureAwait(false);
                if (produtos.Count <= 0 && produtoVsFornecedor==null)
                {

                    mensagensIntegracao.Add(new NFeFilesMensagens()
                    {
                        NFeFiles = nfeFiles,
                        Ativo = true,
                        DataInclusao = DateTime.Now,
                        ChaveNFe = nfeFiles.ChaveAcesso,
                        Texto = $"NCM não cadastrado no Protheus.Código do produto: {produto.prod.NCM} NCM: {produto.prod.xProd}  "
                    });
                }
                if (produtoVsFornecedor == null)
                {
                    mensagensIntegracao.Add(new NFeFilesMensagens()
                    {
                        NFeFiles = nfeFiles,
                        Ativo = true,
                        DataInclusao = DateTime.Now,
                        ChaveNFe = nfeFiles.ChaveAcesso,
                        Texto = $"Fornecedor X Produto não cadastrado no Protheus. CNPJ: {nfeFiles.CnpjFornecedor}  Produto do fornecedor: {produto.prod.cProd} NCM: {produto.prod.NCM}  "
                    });

                    ProdutoIntegrado jaEstaCadastrado = await this.produtoIntegradoService.ExistsProd(produto.prod.cProd, nfeFiles.CnpjFornecedor);


                    if (jaEstaCadastrado == null)
                    {

                        prods = await this.nFeDAO.GetProduto(notaXml);

                        foreach (var prod in prods)
                        {
                            ProdutoIntegrado produtoIntegrado = new ProdutoIntegrado()
                            {
                                CodigoTotvsEmpresaFilial = nfeFiles.Empresa.CodigoTotvsEmpresaFilial,
                                DataInclusao = DateTime.Now,
                                IntegradaoTOTVS = false,
                                Produto = prod,
                                CnpjFornecedor = nfeFiles.CnpjFornecedor
                            };
                            await this.produtoIntegradoService.AdicionarAsync(produtoIntegrado);
                        }

                    }

                }
            }
            return mensagensIntegracao;
        }

        /// <summary>
        /// Validações do Fornecedor
        /// </summary>
        /// <param name="nfeFiles"></param>
        /// <param name="notaXml"></param>
        /// <returns></returns>
        private async Task<List<NFeFilesMensagens>> ValidaFornecedor(NFeFiles nfeFiles, NFe notaXml)
        {
            List<NFeFilesMensagens> mensagensIntegracao = new List<NFeFilesMensagens>();

            var fornecedorExiste = await this.fornecedorService.LocateByCnpjAsync(nfeFiles.Empresa.CodigoTotvsEmpresaFilial, nfeFiles.CnpjFornecedor);
            if (fornecedorExiste == null)
            {


                mensagensIntegracao.Add(new NFeFilesMensagens()
                {
                    NFeFiles = nfeFiles,
                    Ativo = true,
                    DataInclusao = DateTime.Now,
                    ChaveNFe = nfeFiles.ChaveAcesso,
                    Texto = $"Fornecedor {nfeFiles.CnpjFornecedor}-{nfeFiles.Fornecedor} não cadastrado no TOTVS - Protheus"
                });

                var emitentePendente = await this.emitenteService.Get(notaXml.infNFe.emit.Id);

                if (emitentePendente == null)
                {
                    EmitenteIntegrado emitenteIntegrado = new EmitenteIntegrado();
                    emitenteIntegrado.DataInclusao = DateTime.Now;
                    emitenteIntegrado.Emitente = notaXml.infNFe.emit;
                    emitenteIntegrado.IntegradaoTOTVS = false;
                    emitenteIntegrado.CodigoTotvsEmpresaFilial = nfeFiles.Empresa.CodigoTotvsEmpresaFilial;
                    await this.emitenteService.AdicionarAsync(emitenteIntegrado);
                }
                else
                {
                    emitentePendente.IntegradaoTOTVS = false;
                    await this.emitenteService.AtualizarAsync(emitentePendente);
                }

                
            }
            return mensagensIntegracao;
        }

        public async Task<IList<NFe>> ObterNotasByFiles()
        {
            try
            {
                List<NFe> notas = new List<NFe>();
                IQueryable<NFe> nfeQuery = this.nFeDAO.GetAll();
                IList<NFeFiles> nfeFiles = await this.ListarXMLSNaoProcessados(false, false);
                foreach (var nfeFile in nfeFiles)
                {
                    notas.Add(
                    nfeQuery
                        .Where(x => x.infNFe.Id == nfeFile.ChaveAcesso)
                        .FirstOrDefault()
                    );
                }

                return notas;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<ExcelResult> ValidarArquivos(IList<FileStorange> fileStoranges)
        {
            IList<NfeExcelValidarModel> chavesNfe = new List<NfeExcelValidarModel>();

            foreach (var file in fileStoranges)
            {
                if (file.FileType != ".xlsx")
                    break;

                FileInfo newFile = new FileInfo(file.Path);
                FileInfo templateFile = new FileInfo(file.Path);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage(newFile, templateFile))
                {

                    //Open the first worksheet
                    try
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        var cell1 = worksheet.Cells["A1"].Value;
                        var cell2 = worksheet.Cells["B1"].Value;

                        if (!cell1.Equals("ChaveNFE") || cell1.Equals("Validado"))
                        {
                            return new ExcelResult()
                            {
                                Result = false,
                                ResultMessage = "Arquivo Excel sem as colunas ChaveNFE ou Validado."
                            };
                        }

                        var linha = 2;

                        var x = worksheet.Cells[1, 1].Value;
                        for (int i = 0; i < package.Workbook.Worksheets[0].Cells.Count(); i++)
                        {

                            if (package.Workbook.Worksheets[0].Cells[linha, 1].Value != null)
                            {
                                chavesNfe.Add(new NfeExcelValidarModel()
                                {

                                    ChaveNFE = package.Workbook.Worksheets[0].Cells[linha, 1].Value.ToString(),
                                    ValidoString = package.Workbook.Worksheets[0].Cells[linha, 2].Value.ToString()
                                });
                            }
                            linha++;

                        };
                    }
                    catch (Exception ex)
                    {

                        return new ExcelResult()
                        {
                            Result = false,
                            ResultMessage = ex.Message
                        };
                    }

                }

                file.Processado = true;
                await this.fileStorangeDAO.UpdateAsync(file);
            }

            Parallel.ForEach(chavesNfe, (chave) =>
                {

                    if (chave.ValidoString.ToUpper() == "SIM")
                    {
                        chave.Valido = true;
                    }

                    if (chave.ValidoString.ToUpper() == "S")
                    {
                        chave.Valido = true;
                    }
                });

            foreach (var chave in chavesNfe)
            {
                NFeFiles nfeFiles = await this.ObterNFEFilesPelaChave(chave.ChaveNFE);
                if (nfeFiles != null)
                {
                    nfeFiles.Validado = chave.Valido;
                    this.nFeFilesDAO.Update(nfeFiles);
                }

            }

            return new ExcelResult()
            {
                Result = true,
                ResultMessage = "Chaves validadas com sucesso!"
            };
        }

        public async Task<NFeFiles> ObterNFEFilesPelaChave(string chaveNfe)
        {
            NFeFiles nfe = await this.nFeFilesDAO.GetAll().Where(x => x.ChaveAcesso == chaveNfe).SingleOrDefaultAsync();
            return nfe;
        }


        /// <summary>
        /// Faz a revalidação de um XML 
        /// </summary>
        /// <param name="nfeFiles"></param>
        /// <returns></returns>
        public async Task Revalidar(NFeFiles nfeFiles)
        {
            //Inativsa as mensagens de erro 
            List<NFeFilesMensagens> mensagensIntegracao = new List<NFeFilesMensagens>();

            var mensagensDeErro = await nFeFilesMensagensService
                                            .ListarErroDoArquivoAsync(nfeFiles);

            foreach (var erro in mensagensDeErro)
            {
                erro.Ativo = false;
                await nFeFilesMensagensService.Alterar(erro)
                    .ConfigureAwait(false);
            }

            FileStorange file = await fileStorangeDAO.GetAll()
                 .Where(x => x.FileName == nfeFiles.Arquivo)
                 .SingleOrDefaultAsync();

            file.Processado = false;
            await this.fileStorangeDAO.UpdateAsync(file);
            try
            {
                await this.ProcessarArquivos();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro await this.ProcessarArquivos() {ex.Message} ");
            }

        }


        public async Task Revalidar(string chaveNFE)
        {
            NFeFiles nfeFiles = await this.nFeFilesDAO.GetAll()
                                        .Where(n => n.ChaveAcesso == chaveNFE)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();
            await Revalidar(nfeFiles);
        }

        public NFe GetNfeByChave(string chave)
        {
            var nfe = this.nFeDAO.GetAll().Where(x => x.infNFe.Id == chave)
                  .SingleOrDefault();
            return nfe;
        }

        public async Task<bool> Atualiza(NFe nfe)
        {
            return await this.nFeDAO.UpdateAsync(nfe);
        }


    }
}

