using BLL.TOTVS.Cadastros.Interfaces;
using DAL.TOTVS.Cadastros.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.TOTVS.Cadastros.Produtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.TOTVS.Cadastros.Servicos
{
    public class ProdutoTotvsService : IProdutoTotvsService
    {
        private readonly IProdutoTotvsDAO produtoTotvsDAO;

        public ProdutoTotvsService(IProdutoTotvsDAO _produtoTotvsDao)
        {
            produtoTotvsDAO = _produtoTotvsDao;
        }

        public async Task<IList<ProdutoTotvs>> GetAllByNCM(string filial, string ncm)
        {
            IList<ProdutoTotvs> produtos = await produtoTotvsDAO.All()
               .Where(p => p.B1_POSIPI == ncm && p.B1_FILIAL == filial)
               .AsNoTracking()
               .ToListAsync();
            return produtos;
        }

        public async Task<ProdutoTotvs> GetByRef(string filial, string referencia)
        {
            ProdutoTotvs produto = await produtoTotvsDAO.All()
                .Where(p => p.B1_COD == referencia && p.B1_FILIAL == filial)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return produto;
        }
    }
}
