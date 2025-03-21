﻿using Models.Empresas;
using Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Models.NFe
{
    public class NFeFiles : IIdentifier
    {
        [Key]
        [XmlIgnore]
        public int Id { get; set; }
        public string Arquivo { get; set; }
        public string Path { get; set; }
        public string ChaveAcesso { get; set; }
        /// <summary>
        /// Integrado ao ERP
        /// </summary>
        public bool Processada { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataEmnissaoNfe { get; set; }
        public DateTime? DataProcessamento { get; set; }
        public string NumeroNota { get; set; }
        public int Serie { get; set; }
        public string CnpjFornecedor { get; set; }
        public string Fornecedor { get; set; }
        public decimal ValorTotal { get; set; }
        /// <summary>
        /// Aditado
        /// </summary>
        public bool Validado { get; set; }
        public virtual Empresa Empresa { get; set; }
        /// <summary>
        /// Validado automáticamente pelo sistema
        /// </summary>
        public bool? AutoValidado { get; set; }
        public bool? TemXPed { get; set; }
        public bool? FornecedorCadastrado { get; set; }

    }
}
