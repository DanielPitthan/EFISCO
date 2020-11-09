using ContextBinds.EntityCore;
using DAL.Cadastros.Fornecedores.Interfaces;
using DAL.DAOBaseNfeXml;
using Models.Cadastros.Fornecedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Cadastros.Fornecedores.DAO
{
    public class EmitenteIntegradoDAO : DataAccessBaseNfeXml, IEmitenteIntegradoDAO
    {
        public EmitenteIntegradoDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public IQueryable<EmitenteIntegrado> All()
        {
            return this.Contexto.EmitenteIntegrado;
        }
    }
}
