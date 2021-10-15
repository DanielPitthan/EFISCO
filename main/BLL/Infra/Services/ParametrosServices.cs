using BLL.Infra.Interface;
using DAL.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Infra.Services
{
    public class ParametrosServices : IParametroService
    {
        private readonly IParametrosDAO parametroDAO;

        public ParametrosServices(IParametrosDAO _parametroDAO)
        {
            parametroDAO = _parametroDAO;
        }

        public async Task<bool> MergeAsync(Parametro parametro)
        {
            if (parametro.Id > 0)
                return await AtualizarAsync(parametro);
            
            return await InserirAsync(parametro);
        }

        private async Task<bool> InserirAsync(Parametro parametro)
        {
            return await this.parametroDAO.AddSysnc(parametro);
        }

        private async Task<bool> AtualizarAsync(Parametro parametro)
        {
            return await this.parametroDAO.UpdateAsync(parametro);
        }

        public async Task<Parametro> ObterParametro(string codigo)
        {
            Parametro parametro = await parametroDAO
                                .GetAll()
                                .Where(x => x.Codigo == codigo)
                                .SingleOrDefaultAsync();
            return parametro;
        }

        public async Task<IList<Parametro>> List()
        {
            var list =  await this.parametroDAO
                                    .GetAll()
                                    .AsNoTracking()
                                    .ToListAsync();
            return list;
        }

        public async Task<bool> ExcluirAsync(Parametro parametro)
        {
            var result = await parametroDAO.DeleteAsync(parametro);
            return result;
        }
    }
}
