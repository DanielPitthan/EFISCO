﻿using System;
using System.Collections.Generic;
using System.Text;
using XmlNFe.Nfes.Informacoes.Emitente;

namespace Models.Cadastros.Fornecedores
{
    public class EmitenteIntegrado
    {
        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public bool IntegradaoTOTVS { get; set; }
        public emit Emitente { get; set; }
        public string CodigoTotvsEmpresaFilial { get; set; }
    }
}
