using BLL.Empresas.Interfaces;
using DAL.Empresas.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Empresas;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Empresas.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaDAO empresaDAO;

        public EmpresaService(IEmpresaDAO _empresaDAO)
        {
            empresaDAO = _empresaDAO;
        }

        public async Task<Empresa> GetByCnpjsync(string cnpj)
        {
            Empresa empresa = await empresaDAO.All().Where(x => x.Cnpj == cnpj)
                               .SingleOrDefaultAsync();
            return empresa;
        }

        public async Task<Empresa> GetByIdAsync(int id)
        {
            Empresa empresa = await empresaDAO.All().Where(x => x.Id == id)
                                .SingleOrDefaultAsync();
            return empresa;
        }

        public async Task<IList<Empresa>> ListAllAsync()
        {
            return await empresaDAO.All().ToListAsync();
        }
    }
}
