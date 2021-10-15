using Models.Empresas;
using Models.Interfaces;
using System;

namespace Models.Certificados
{
    public class Certificado : IIdentifier
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public byte[] Certf { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public DateTime DataExpiracao { get; set; }
        public DateTime DataInclusao { get; set; }
        public bool Ativo { get; set; }
        public Empresa Empresa { get; set; }
    }

}
