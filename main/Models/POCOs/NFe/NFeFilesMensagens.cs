using Models.Interfaces;
using System;

namespace Models.NFe
{
    public class NFeFilesMensagens : IIdentifier
    {

        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public bool Ativo { get; set; }
        public string Texto { get; set; }
        public string ChaveNFe { get; set; }
        public NFeFiles NFeFiles { get; set; }
    }
}
