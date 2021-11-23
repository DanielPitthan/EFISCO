using BLL.Infra.Interface;
using DAL.FileStoranges.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.FileStoranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infra.Services
{
    public class FileStorangeService : IFileStorangeService
    {
        private IFileStorangeDAO fileStorangeDAO;

        public FileStorangeService(IFileStorangeDAO _fileStorangeDAO)
        {
            this.fileStorangeDAO = _fileStorangeDAO;
        }
        public IQueryable<FileStorange> GetByOrigem(OrigemArquivo origemArquivo)
        {

            var query = fileStorangeDAO.GetAll()
                                       .Where(x => x.OrigemId.Value == origemArquivo)
                                       .Where(x => x.Processado == false)
                                       .Include(x => x.EmailRecebido);
                                     
            return query;
        }

        
    }
}
