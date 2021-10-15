using ContextBinds.EntityCore;
using DAL.Cadastros.Produtos.Interfaces;
using DAL.DAOBaseNfeXml;
using Models.Cadastros.Produtos;
using System.Linq;

namespace DAL.Cadastros.Produtos.DAO
{
    public class ProdutoIntegradoDAO : DataAccessBaseNfeXml, IProdutoIntegradoDAO
    {
        public ProdutoIntegradoDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public IQueryable<ProdutoIntegrado> All()
        {
            return Contexto.ProdutoIntegrado;
        }

        public override bool Update<TSource>(TSource item)
        {
            return base.Update(item);
        }
    }
}
