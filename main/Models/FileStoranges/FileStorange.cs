using System;
using System.Collections.Generic;
using System.Text;

namespace Models.FileStoranges
{
    public class FileStorange
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
    }
}
