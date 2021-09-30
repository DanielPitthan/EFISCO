using BLL.NFE.Interfaces;
using DFe.Classes.Flags;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using XmlNFe.Nfes;
using XmlNFe.Nfes.Informacoes.Destinatario;
using XmlNFe.Nfes.Informacoes.Emitente;
using XmlNFe.Nfes.Informacoes.Identificacao.Tipos;
using XmlNFe.Nfes.Informacoes.Transporte;

namespace BLL.NFE.Services
{
    public class NfeDownloadService : INfeDownloadListService
    {
        private readonly INFeXmlService nFeXmlService;


        public NfeDownloadService(INFeXmlService _nFeXmlService
                                 )
        {
            nFeXmlService = _nFeXmlService;

        }

        public async Task<byte[]> DownloadList()
        {
            IList<NFe> Nfes;

            try
            {
                Nfes = await nFeXmlService.ObterNotasByFiles();


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo file = new FileInfo("NotasFiscais.xlsx");
            using (ExcelPackage excel = new ExcelPackage(file))
            {

                //Abas
                ExcelWorksheet planNotas = excel.Workbook.Worksheets.Add("Notas");
                ExcelWorksheet planProduto = excel.Workbook.Worksheets.Add("Produtos");

                //Cabecalho 
                planNotas.Cells[1, 1].Value = "Chave Nfe";
                planNotas.Cells[1, 2].Value = "UF";
                planNotas.Cells[1, 3].Value = "Nat. Operação";
                planNotas.Cells[1, 4].Value = "Serie";
                planNotas.Cells[1, 5].Value = "Num. NF";
                planNotas.Cells[1, 6].Value = "Emissão NF";
                planNotas.Cells[1, 7].Value = "Dt Saída";

                planNotas.Cells[1, 8].Value = "Tp NF";
                planNotas.Cells[1, 9].Value = "Tp Ambiente";
                planNotas.Cells[1, 10].Value = "Finalidade NFe";
                planNotas.Cells[1, 11].Value = "Cons. Final";
                planNotas.Cells[1, 12].Value = "Presenca Comprador";

                //Emitente
                planNotas.Cells[1, 13].Value = "Emit CNPJ";
                planNotas.Cells[1, 14].Value = "Emit Razão Soc.";
                planNotas.Cells[1, 15].Value = "Emit Fantasia";
                planNotas.Cells[1, 16].Value = "Emit End.";
                planNotas.Cells[1, 17].Value = "Emit Nro.";
                planNotas.Cells[1, 18].Value = "Emit Bairro";
                planNotas.Cells[1, 19].Value = "Emit Município Ibge";
                planNotas.Cells[1, 20].Value = "Emit Município";
                planNotas.Cells[1, 21].Value = "Emit UF";
                planNotas.Cells[1, 22].Value = "Emit CEP";
                planNotas.Cells[1, 23].Value = "Emit Cod Pais Ibge";
                planNotas.Cells[1, 24].Value = "Emit Pais ";
                planNotas.Cells[1, 25].Value = "Emit Fone ";
                planNotas.Cells[1, 26].Value = "Emit IE ";
                planNotas.Cells[1, 27].Value = "Emit Regime Tributário";

                //Dest
                planNotas.Cells[1, 28].Value = "Dest CNPJ";
                planNotas.Cells[1, 29].Value = "Dest Nome";
                planNotas.Cells[1, 30].Value = "Dest Endereço";
                planNotas.Cells[1, 31].Value = "Dest Numero";
                planNotas.Cells[1, 32].Value = "Dest Complemento";
                planNotas.Cells[1, 33].Value = "Dest Bairro";
                planNotas.Cells[1, 34].Value = "Dest Cod Mun Ibge";
                planNotas.Cells[1, 35].Value = "Dest Cidade";
                planNotas.Cells[1, 36].Value = "Dest UF";
                planNotas.Cells[1, 37].Value = "Dest CEP";
                planNotas.Cells[1, 38].Value = "Dest Cod Pais Ibge";
                planNotas.Cells[1, 39].Value = "Dest Pais";
                planNotas.Cells[1, 40].Value = "Dest Fone";
                planNotas.Cells[1, 41].Value = "Dest Contribuinte";
                planNotas.Cells[1, 42].Value = "Dest IE";

                //total
                planNotas.Cells[1, 43].Value = "Base Icms";
                planNotas.Cells[1, 44].Value = "Val Icms";
                planNotas.Cells[1, 45].Value = "Val Icms Desonerado";
                planNotas.Cells[1, 46].Value = "Val FCAP";
                planNotas.Cells[1, 47].Value = "Base ST";
                planNotas.Cells[1, 48].Value = "Icms ST";
                planNotas.Cells[1, 49].Value = "Total Prod";
                planNotas.Cells[1, 50].Value = "Val Frete";
                planNotas.Cells[1, 51].Value = "Seguro";
                planNotas.Cells[1, 52].Value = "Desc";
                planNotas.Cells[1, 53].Value = "Val IPI";
                planNotas.Cells[1, 54].Value = "Val IPI Dev";
                planNotas.Cells[1, 55].Value = "Val PIS";
                planNotas.Cells[1, 56].Value = "Val COFINS";
                planNotas.Cells[1, 57].Value = "Val Outros";
                planNotas.Cells[1, 58].Value = "vII";
                planNotas.Cells[1, 59].Value = "Val NF";

                //Transp
                planNotas.Cells[1, 60].Value = "TP Frete";

                //infAdic

                planNotas.Cells[1, 61].Value = "Info Adicional Fisco";
                planNotas.Cells[1, 62].Value = "Info Adicional Compl";



                //prod
                planProduto.Cells[1, 1].Value = "Chave Nfe";
                planProduto.Cells[1, 2].Value = "Produto";
                planProduto.Cells[1, 3].Value = "CFOP";
                planProduto.Cells[1, 4].Value = "UN";
                planProduto.Cells[1, 5].Value = "Qtd";
                planProduto.Cells[1, 6].Value = "Vlr Unitario";
                planProduto.Cells[1, 7].Value = "Total";

                //Formatação por range
                using (ExcelRange r = planNotas.Cells[1, 1, 1, 62])
                {
                    r.Style.Font.Bold = true;
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(122, 186, 255));
                }

                using (ExcelRange r = planProduto.Cells[1, 1, 1, 7])
                {
                    r.Style.Font.Bold = true;
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(122, 186, 255));
                }


                int linhaNFE = 2;
                int linhaProd = 2;


                for (int i = 0; i < Nfes.Count; i++)
                {


                    planNotas.Cells[linhaNFE, 1].Value = Nfes[i].infNFe.Id;
                    planNotas.Cells[linhaNFE, 2].Value = Nfes[i].infNFe.ide.cUF;
                    planNotas.Cells[linhaNFE, 3].Value = Nfes[i].infNFe.ide.natOp;
                    planNotas.Cells[linhaNFE, 4].Value = Nfes[i].infNFe.ide.serie;
                    planNotas.Cells[linhaNFE, 5].Value = Nfes[i].infNFe.ide.nNF;
                    planNotas.Cells[linhaNFE, 6].Value = Nfes[i].infNFe.ide.dhEmi;
                    planNotas.Cells[linhaNFE, 7].Value = Nfes[i].infNFe.ide.dhSaiEnt;

                    planNotas.Cells[linhaNFE, 8].Value = Enum.GetName(typeof(TipoNFe), Nfes[i].infNFe.ide.tpNF);
                    planNotas.Cells[linhaNFE, 9].Value = Enum.GetName(typeof(TipoAmbiente), Nfes[i].infNFe.ide.tpAmb);
                    planNotas.Cells[linhaNFE, 10].Value = Enum.GetName(typeof(FinalidadeNFe), Nfes[i].infNFe.ide.finNFe);
                    planNotas.Cells[linhaNFE, 11].Value = Enum.GetName(typeof(ConsumidorFinal), Nfes[i].infNFe.ide.indFinal);
                    planNotas.Cells[linhaNFE, 12].Value = Enum.GetName(typeof(PresencaComprador), Nfes[i].infNFe.ide.indPres);

                    //emit
                    planNotas.Cells[linhaNFE, 13].Value = Nfes[i].infNFe.emit.CNPJ;
                    planNotas.Cells[linhaNFE, 14].Value = Nfes[i].infNFe.emit.xNome;
                    planNotas.Cells[linhaNFE, 15].Value = Nfes[i].infNFe.emit.xFant;
                    planNotas.Cells[linhaNFE, 16].Value = Nfes[i].infNFe.emit.enderEmit.xLgr;
                    planNotas.Cells[linhaNFE, 17].Value = Nfes[i].infNFe.emit.enderEmit.nro;
                    planNotas.Cells[linhaNFE, 18].Value = Nfes[i].infNFe.emit.enderEmit.xBairro;
                    planNotas.Cells[linhaNFE, 19].Value = Nfes[i].infNFe.emit.enderEmit.cMun;
                    planNotas.Cells[linhaNFE, 20].Value = Nfes[i].infNFe.emit.enderEmit.xMun;
                    planNotas.Cells[linhaNFE, 21].Value = Nfes[i].infNFe.emit.enderEmit.UF;
                    planNotas.Cells[linhaNFE, 22].Value = Nfes[i].infNFe.emit.enderEmit.CEP;
                    planNotas.Cells[linhaNFE, 23].Value = Nfes[i].infNFe.emit.enderEmit.cPais;
                    planNotas.Cells[linhaNFE, 24].Value = Nfes[i].infNFe.emit.enderEmit.xPais;
                    planNotas.Cells[linhaNFE, 25].Value = Nfes[i].infNFe.emit.enderEmit.fone;
                    planNotas.Cells[linhaNFE, 25].Value = Nfes[i].infNFe.emit.IE;
                    planNotas.Cells[linhaNFE, 25].Value = Enum.GetName(typeof(CRT), Nfes[i].infNFe.emit.CRT);

                    //Dest
                    planNotas.Cells[linhaNFE, 28].Value = Nfes[i].infNFe.dest.CNPJ;
                    planNotas.Cells[linhaNFE, 29].Value = Nfes[i].infNFe.dest.xNome;
                    planNotas.Cells[linhaNFE, 30].Value = Nfes[i].infNFe.dest.enderDest.xLgr;
                    planNotas.Cells[linhaNFE, 31].Value = Nfes[i].infNFe.dest.enderDest.nro;
                    planNotas.Cells[linhaNFE, 32].Value = Nfes[i].infNFe.dest.enderDest.xCpl;
                    planNotas.Cells[linhaNFE, 33].Value = Nfes[i].infNFe.dest.enderDest.xBairro;
                    planNotas.Cells[linhaNFE, 34].Value = Nfes[i].infNFe.dest.enderDest.cMun;
                    planNotas.Cells[linhaNFE, 35].Value = Nfes[i].infNFe.dest.enderDest.xMun;
                    planNotas.Cells[linhaNFE, 36].Value = Nfes[i].infNFe.dest.enderDest.UF;
                    planNotas.Cells[linhaNFE, 37].Value = Nfes[i].infNFe.dest.enderDest.CEP;
                    planNotas.Cells[linhaNFE, 38].Value = Nfes[i].infNFe.dest.enderDest.cPais;
                    planNotas.Cells[linhaNFE, 39].Value = Nfes[i].infNFe.dest.enderDest.xPais;
                    planNotas.Cells[linhaNFE, 40].Value = Nfes[i].infNFe.dest.enderDest.fone;
                    planNotas.Cells[linhaNFE, 41].Value = Enum.GetName(typeof(indIEDest), Nfes[i].infNFe.dest.indIEDest);
                    planNotas.Cells[linhaNFE, 42].Value = Nfes[i].infNFe.dest.IE;


                    //prod
                    for (int j = 0; j < Nfes[i].infNFe.det.Count; j++)
                    {
                        planProduto.Cells[linhaProd, 1].Value = Nfes[i].infNFe.Id;
                        planProduto.Cells[linhaProd, 2].Value = Nfes[i].infNFe.det[j].prod.xProd;
                        planProduto.Cells[linhaProd, 3].Value = Nfes[i].infNFe.det[j].prod.CFOP;
                        planProduto.Cells[linhaProd, 4].Value = Nfes[i].infNFe.det[j].prod.uCom;
                        planProduto.Cells[linhaProd, 5].Value = Nfes[i].infNFe.det[j].prod.qCom;
                        planProduto.Cells[linhaProd, 6].Value = Nfes[i].infNFe.det[j].prod.vUnCom;
                        planProduto.Cells[linhaProd, 7].Value = Nfes[i].infNFe.det[j].prod.vProd;
                        linhaProd++;
                    }


                    //total
                    planNotas.Cells[linhaNFE, 43].Value = Nfes[i].infNFe.total.ICMSTot.vBC;
                    planNotas.Cells[linhaNFE, 44].Value = Nfes[i].infNFe.total.ICMSTot.vICMS;
                    planNotas.Cells[linhaNFE, 45].Value = Nfes[i].infNFe.total.ICMSTot.vICMSDeson;
                    planNotas.Cells[linhaNFE, 46].Value = Nfes[i].infNFe.total.ICMSTot.vFCP;
                    planNotas.Cells[linhaNFE, 47].Value = Nfes[i].infNFe.total.ICMSTot.vBCST;
                    planNotas.Cells[linhaNFE, 48].Value = Nfes[i].infNFe.total.ICMSTot.vST;
                    planNotas.Cells[linhaNFE, 49].Value = Nfes[i].infNFe.total.ICMSTot.vProd;
                    planNotas.Cells[linhaNFE, 50].Value = Nfes[i].infNFe.total.ICMSTot.vFrete;
                    planNotas.Cells[linhaNFE, 51].Value = Nfes[i].infNFe.total.ICMSTot.vSeg;
                    planNotas.Cells[linhaNFE, 52].Value = Nfes[i].infNFe.total.ICMSTot.vDesc;
                    planNotas.Cells[linhaNFE, 53].Value = Nfes[i].infNFe.total.ICMSTot.vIPI;
                    planNotas.Cells[linhaNFE, 54].Value = Nfes[i].infNFe.total.ICMSTot.vIPIDevol;
                    planNotas.Cells[linhaNFE, 55].Value = Nfes[i].infNFe.total.ICMSTot.vPIS;
                    planNotas.Cells[linhaNFE, 56].Value = Nfes[i].infNFe.total.ICMSTot.vCOFINS;
                    planNotas.Cells[linhaNFE, 57].Value = Nfes[i].infNFe.total.ICMSTot.vOutro;
                    planNotas.Cells[linhaNFE, 58].Value = Nfes[i].infNFe.total.ICMSTot.vII;
                    planNotas.Cells[linhaNFE, 59].Value = Nfes[i].infNFe.total.ICMSTot.vNF;


                    planNotas.Cells[linhaNFE, 60].Value = Enum.GetName(typeof(ModalidadeFrete), Nfes[i].infNFe.transp.modFrete);
                    if (Nfes[i].infNFe.infAdic != null)
                    {
                        planNotas.Cells[linhaNFE, 61].Value = Nfes[i].infNFe.infAdic.infAdFisco;
                        planNotas.Cells[linhaNFE, 62].Value = Nfes[i].infNFe.infAdic.infCpl;
                    }


                    linhaNFE++;

                }

                byte[] bytesOfFile = excel.GetAsByteArray();
                return bytesOfFile;

            }
        }
    }
}