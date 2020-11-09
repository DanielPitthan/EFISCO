using ContextBinds.EntityCore;
using DAL.Cadastros.Produtos.Interfaces;
using DAL.DAOBaseNfeXml;
using Models.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Cadastros.Produtos.DAO
{
    public class ProdutoIntegradoDAO : DataAccessBaseNfeXml,IProdutoIntegradoDAO
    {
        public ProdutoIntegradoDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public IQueryable<ProdutoIntegrado> All()
        {
            return this.Contexto.ProdutoIntegrado;
        }
    }
}
