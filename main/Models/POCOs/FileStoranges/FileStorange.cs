using Models.Interfaces;
using Models.POCOs.FileStoranges;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.FileStoranges
{
    public class FileStorange:IIdentifier
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string OriginalFileName { get; set; }
        public string MD5 { get; set; }
        public DateTime? DataInclusao { get; set; }
        public int? UsuarioId { get; set; }
        public Guid FileStornageUnique { get; set; }
        public bool Processado { get; set; }
        public string XmlString { get; set; }
        public string FileType { get; set; }
#nullable enable
        public byte[]? DataByte { get; set; }
#nullable disable
        public OrigemArquivo? OrigemId { get; set; }
        //public string RemetenteEmail { get; set; }
        public string TipoXml { get; set; }
        //public DateTime DataRecebimetoEmail { get; set; }
        //public string CorpoDoEmail { get; set; }

        [NotMapped]
        public bool _processando = false;
        public  EmailRecebido EmailRecebido { get; set; }    
    }
}
