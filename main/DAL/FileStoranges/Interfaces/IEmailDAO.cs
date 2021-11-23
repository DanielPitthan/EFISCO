using Models.POCOs.FileStoranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FileStoranges.Interfaces
{
    public interface IEmailDAO
    {
        public Task<bool> AddAsync(EmailRecebido emial);
      
        public Task<bool> UpdateAsync(EmailRecebido emial);
        public EmailRecebido GetById(int id);
        public EmailRecebido GetByIdWithAttach(int id);

    }
}
