using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.KPIs
{
    public class TopFornecedores
    {
        public string Cnpj { get; set; }
        public string Fornecedor { get; set; }
        public int  Quantidade { get; set; }
        public decimal Total { get; set; }
    }
}
