using ContextBinds.EntityCore;
using DAL.DAOBaseNfeXml;
using DAL.FileStoranges.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.POCOs.FileStoranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FileStoranges.DAO
{
    public class EmailDAO : DataAccessBaseNfeXml, IEmailDAO
    {
        public EmailDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public async Task<bool> AddAsync(EmailRecebido email)
        {
            var result = await base.AddSysnc<EmailRecebido>(email);

            foreach (var anexo in email.AnexosDoEmail)
            {
                await base.AddSysnc<AnexosDoEmail>(anexo);
            }

            return result;
        }

        public EmailRecebido GetById(int id)
        {
            return base.GetById<EmailRecebido>(id);
        }

        public EmailRecebido GetByIdWithAttach(int id)
        {
            var email = base.Contexto.EmailRecebido
                         .Where(x => x.Id == id)
                         .Include(x => x.AnexosDoEmail)
                         .FirstOrDefault();

            return email;
        }

        public async Task<bool> UpdateAsync(EmailRecebido emial)
        {
            return await base.UpdateAsync(emial);
        }
    }
}
