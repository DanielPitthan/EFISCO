using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Relatorios.Ativo
{
    public class AtivoVsNotaFiscalFilter
    {
        public DateTime? EmissaoDe { get; set; }
        public DateTime? EmissaoAte { get; set; }
        public string AtivoDe { get; set; }
        public string AtivoAte { get; set; }
    }
}
