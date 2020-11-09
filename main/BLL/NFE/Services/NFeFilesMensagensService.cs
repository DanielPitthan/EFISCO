using BLL.NFE.Interfaces;
using DAL.XmlDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.NFe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.NFE.Services
{
    public class NFeFilesMensagensService : INFeFilesMensagensService
    {
        private INFeFilesMensagensDAO nFeFilesMensagensDAO;
        private INFeFilesDAO nfeFilesDAO;

        public NFeFilesMensagensService(INFeFilesMensagensDAO _nFeFilesMensagensDAO,
                                        INFeFilesDAO _nfeFilesDAO)
        {
            this.nFeFilesMensagensDAO = _nFeFilesMensagensDAO;
            this.nfeFilesDAO = _nfeFilesDAO;
        }

        public async Task<bool> Adcionar(NFeFilesMensagens nFeFilesMensagens)
        {
            return  await nFeFilesMensagensDAO.AddSysnc(nFeFilesMensagens);
        }

        public async Task<bool> Alterar(NFeFilesMensagens nFeFilesMensagens)
        {
            return nFeFilesMensagensDAO.Update(nFeFilesMensagens);
        }

        public async Task<bool> Excluir(NFeFilesMensagens nFeFilesMensagens)
        {
            return await nFeFilesMensagensDAO.DeleteAsync(nFeFilesMensagens);
        }

        public async Task<IList<NFeFilesMensagens>> ListarErroDoArquivoAsync(NFeFiles nfeFile)
        {
            IList<NFeFilesMensagens> erros = await nFeFilesMensagensDAO.GetAll()
                .Where(x => x.NFeFiles.Id == nfeFile.Id && x.Ativo==true)
                .Include(x=> x.NFeFiles)
                .ThenInclude(x=> x.Empresa)
                .ToListAsync();
            return erros;
        } 
        public  IList<NFeFilesMensagens> ListarErroDoArquivo(NFeFiles nfeFile)
        {
            IList<NFeFilesMensagens> erros =  nFeFilesMensagensDAO.GetAll()
                .Where(x => x.NFeFiles.Id == nfeFile.Id && x.Ativo == true)
                .ToList();
            return erros;
        }

        public async Task<IList<NFeFilesMensagens>> ListarErroDoArquivoAsync(string chaveNFE)
        {
            NFeFiles nfefiles = await nfeFilesDAO.GetAll()
                                    .Where(n => n.ChaveAcesso == chaveNFE)
                                    .SingleOrDefaultAsync();

            return await ListarErroDoArquivoAsync(nfefiles);
        }
    }
}
