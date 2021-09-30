using ContextBinds.EntityCore;
using DAL.TOTVS.Relatorios.Interfaces;
using Models.Relatorios.Ativo;
using System.Linq;

namespace DAL.TOTVS.Relatorios.DAO
{
    public class AtivoVsNotaFiscalDAO : IAtivoVsNotaFiscalDAO
    {
        private readonly ContextTOTVS contextTotvs;

        public AtivoVsNotaFiscalDAO(ContextTOTVS context)
        {
            contextTotvs = context;
        }

        public IQueryable<AtivoVsNotaFiscal> All()
        {
            return contextTotvs.AtivoVsNotaFiscals;
        }
    }
}
