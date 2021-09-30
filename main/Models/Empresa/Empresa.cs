using System.ComponentModel.DataAnnotations;

namespace Models.Empresas
{
    public class Empresa
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string CodigoEmpresaTotvs { get; set; }
        public virtual string CodigoFilialTotvs { get; set; }
        public virtual string CodigoTotvsEmpresaFilial => CodigoEmpresaTotvs + CodigoFilialTotvs;
        public virtual string RazaoSocial { get; set; }
        public virtual string NomeFantasia { get; set; }
        public virtual string RazaoSocialCodigo => CodigoTotvsEmpresaFilial + " - " + RazaoSocial;
        public virtual string DescricaoResumida { get; set; }

        public virtual string Cnpj { get; set; }
        public virtual string Ie { get; set; }
        public virtual string UF { get; set; }
        public virtual bool Centralizadora { get; set; }

        public virtual string Sigla { get; set; }
    }
}
