using ContextBinds.EntityCore;
using DAL.TOTVS.Relatorios.Interfaces;
using Models.Relatorios.Ativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.TOTVS.Relatorios.DAO
{
    public class AtivoVsNotaFiscalDAO : IAtivoVsNotaFiscalDAO
    {
        private ContextTOTVS contextTotvs;

        public AtivoVsNotaFiscalDAO(ContextTOTVS context)
        {
            this.contextTotvs = context;
        }

        public IQueryable<AtivoVsNotaFiscal> All()
        {
            return contextTotvs.AtivoVsNotaFiscals;
        }
    }
}
