using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.POCOs.FileStoranges
{
    public class EmailRecebido:IIdentifier
    {
        public int Id { get; set; }
        public string Assunto   { get; set; }
        public string De { get; set; }
        public string Para { get; set; }
        public string CC { get; set; }
        public string Corpo { get; set; }
        public DateTime DataRecebido { get; set; }
        public IList<AnexosDoEmail> AnexosDoEmail { get; set; }
    }
}
