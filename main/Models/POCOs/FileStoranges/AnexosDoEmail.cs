using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.POCOs.FileStoranges
{
    public  class AnexosDoEmail:IIdentifier
    {
        public int Id { get; set; }
        public byte[] Anexo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public EmailRecebido EmailRecebido { get; set; }
    }
}
