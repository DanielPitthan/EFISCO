using BLL.TOTVS.Relatorios.Interfaces;
using DAL.TOTVS.Relatorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Relatorios.Ativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TOTVS.Relatorios.Services
{
    public class AtivoVsNotaFiscalService : IAtivoVsNotaFiscalService
    {
        private IAtivoVsNotaFiscalDAO ativoVsNfDAO;

        public AtivoVsNotaFiscalService(IAtivoVsNotaFiscalDAO reportDAO)
        {
            this.ativoVsNfDAO = reportDAO;
        }

        public async Task<IList<AtivoVsNotaFiscal>> GerarRelatorioAsync(AtivoVsNotaFiscalFilter parametros)
        {
            IQueryable<AtivoVsNotaFiscal> report = ativoVsNfDAO.All();

            if (!string.IsNullOrEmpty(parametros.AtivoDe))
                report = report.Where(x => x.Ativo.Contains(parametros.AtivoDe));

            if (!string.IsNullOrEmpty(parametros.AtivoAte))
                report = report.Where(x => x.Ativo.Contains(parametros.AtivoAte));

            if (parametros.EmissaoDe.HasValue)
                report = report.Where(x => x.Emissao >= parametros.EmissaoDe.Value);

            if (parametros.EmissaoAte.HasValue)
                report = report.Where(x => x.Emissao <= parametros.EmissaoAte.Value);

            var relatorio = await report.ToListAsync();
            return relatorio;
        }
    }
}
