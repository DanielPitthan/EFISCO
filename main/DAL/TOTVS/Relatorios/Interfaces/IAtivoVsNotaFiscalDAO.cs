using Models.Relatorios.Ativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.TOTVS.Relatorios.Interfaces
{
    public interface IAtivoVsNotaFiscalDAO
    {
        public IQueryable<AtivoVsNotaFiscal> All();
    }
}
