using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empresas
{
    public class Empresa
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string CodigoEmpresaTotvs { get; set; }
        public virtual string CodigoFilialTotvs { get; set; }
        public virtual string CodigoTotvsEmpresaFilial
        {
            get
            {
                return this.CodigoEmpresaTotvs + this.CodigoFilialTotvs;
            }
        }
        public virtual string RazaoSocial { get; set; }
        public virtual string NomeFantasia { get; set; }
        public virtual string RazaoSocialCodigo
        {
            get
            {
                return this.CodigoTotvsEmpresaFilial + " - " + this.RazaoSocial;
            }
        }
        public virtual string DescricaoResumida { get; set; }

        public virtual string Cnpj { get; set; }
        public virtual string Ie { get; set; }
        public virtual string UF { get; set; }
        public virtual bool Centralizadora { get; set; }

        public virtual string Sigla { get; set; }
    }
}
