using BLL.Infra.Interface;
using DAL.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Infra;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Infra.Services
{
    public class ParametrosServices : IParametroService
    {
        private readonly IParametros parametroDAO;

        public ParametrosServices(IParametros _parametroDAO)
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
    }
}
