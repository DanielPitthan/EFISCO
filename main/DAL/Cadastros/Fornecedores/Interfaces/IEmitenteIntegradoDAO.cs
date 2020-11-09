using DAL.DAOBaseNfeXml;
using DAL.XmlDAL.Interfaces;
using Models.Cadastros.Fornecedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Cadastros.Fornecedores.Interfaces
{
    public interface IEmitenteIntegradoDAO: IDataAccessBaseNfeXml
    {
        public IQueryable<EmitenteIntegrado> All();
    }
}
