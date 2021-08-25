using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFISCO.Pages.Dashboards.Kpis.Components
{
    public class NfeKPIBase:ComponentBase
    {
        [Inject] public BLL.NFE.Interfaces.INFeXmlService NfeXlsService { get; set; }

        public  int TotalJaProcessadas { get; set; }
        public  int TotalValidadas { get; set; }
        public  int TotalAuditadas { get; set; }
        public  decimal TotalJaProcessadasValor { get; set; }
        public  decimal TotalValidadasValor { get; set; }
        public  decimal TotalAuditadasValor { get; set; }


        protected override void OnInitialized()
        {
            var notasFiscais = NfeXlsService.ListarTodosXMLQuery();

            TotalJaProcessadas = notasFiscais.Count();

            TotalValidadas = notasFiscais.Where(x => x.AutoValidado == true)
                                    .Count();

            TotalAuditadas = notasFiscais.Where(x => x.Validado == true)
                                .Count();

            TotalJaProcessadasValor = notasFiscais.Sum(x => x.ValorTotal);

            TotalValidadasValor = notasFiscais.Where(x => x.AutoValidado == true)
                                   .Sum(x => x.ValorTotal);

            TotalAuditadasValor = notasFiscais.Where(x => x.Validado == true)
                                .Sum(x => x.ValorTotal);
        }
    }
}
