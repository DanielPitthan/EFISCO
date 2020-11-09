using BLL.TOTVS.Cadastros.Interfaces;
using DAL.TOTVS.Cadastros.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.TOTVS.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TOTVS.Cadastros.Servicos
{
    public class ProdutoTotvsService : IProdutoTotvsService
    {
        private IProdutoTotvsDAO produtoTotvsDAO;

        public ProdutoTotvsService(IProdutoTotvsDAO _produtoTotvsDao)
        {
            this.produtoTotvsDAO = _produtoTotvsDao;
        }

        public async Task<IList<ProdutoTotvs>> GetAllByNCM(string filial, string ncm)
        {
            IList<ProdutoTotvs> produtos = await this.produtoTotvsDAO.All()
               .Where(p => p.B1_POSIPI == ncm && p.B1_FILIAL == filial)
               .ToListAsync();
            return produtos;
        }

        public async Task<ProdutoTotvs> GetByRef(string filial,string referencia)
        {
            ProdutoTotvs produto = await this.produtoTotvsDAO.All()
                .Where(p => p.B1_COD == referencia && p.B1_FILIAL==filial)
                .SingleOrDefaultAsync();
            return produto;
        }
    }
}
