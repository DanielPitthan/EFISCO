using BLL.Cadastros.Fornecedores.Interfaces;
using DAL.Cadastros.Fornecedores.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Cadastros.Fornecedores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Cadastros.Fornecedores.Services
{
    public class EmitenteIntegradoService : IEmitenteIntegradoService
    {
        private readonly IEmitenteIntegradoDAO emitenteDAO;

        public EmitenteIntegradoService(IEmitenteIntegradoDAO _emitenteDAO)
        {
            emitenteDAO = _emitenteDAO;
        }

        public async Task<bool> AdicionarAsync(EmitenteIntegrado emitente)
        {

            return await emitenteDAO.AddSysnc(emitente);
        }

        public async Task<bool> AtualizarAsync(EmitenteIntegrado emitente)
        {
            return await emitenteDAO.UpdateAsync(emitente);
        }

        public async Task<bool> ExcluirAsync(EmitenteIntegrado emitente)
        {
            return await emitenteDAO.DeleteAsync(emitente);
        }

        public async Task<EmitenteIntegrado> Get(int emitId)
        {
            EmitenteIntegrado emitente = await emitenteDAO.All()
                                                     .Include(e => e.Emitente)
                                                     .ThenInclude(ende => ende.enderEmit)
                                                     .Where(x => x.Emitente.Id == emitId)
                                                     .AsNoTracking()
                                                     .FirstOrDefaultAsync();
            return emitente;
        }

        public async Task<IList<EmitenteIntegrado>> ListarNaoIntegradosAsync()
        {
            IList<EmitenteIntegrado> emitentes = await emitenteDAO.All()
                                                    .Where(x => x.IntegradaoTOTVS == false)
                                                    .Include(e => e.Emitente)
                                                    .ThenInclude(ende => ende.enderEmit)
                                                    .ToListAsync();
            return emitentes;
        }
    }
}
