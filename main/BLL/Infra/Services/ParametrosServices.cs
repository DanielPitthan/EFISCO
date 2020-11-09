using BLL.Infra.Interface;
using DAL.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infra.Services
{
    public class ParametrosServices : IParametroService
    {
        private IParametros parametroDAO;

        public ParametrosServices(IParametros _parametroDAO)
        {
            this.parametroDAO = _parametroDAO;
        }

        public async Task<Parametro> ObterParametro(string codigo)
        {
            Parametro parametro= await this.parametroDAO
                                .GetAll()
                                .Where(x => x.Codigo == codigo)
                                .SingleOrDefaultAsync();
            return parametro;
        }
    }
}
