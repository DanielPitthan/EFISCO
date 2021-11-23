using BLL.Infra.Interface;
using DAL.FileStoranges.Interfaces;
using Models.POCOs.FileStoranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infra.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailDAO emialDAO;

        public EmailService(IEmailDAO _mailDAO)
        {
            this.emialDAO = _mailDAO;
        }

        public EmailRecebido GetById(int id)
        {
            var email = emialDAO.GetByIdWithAttach(id);
            return email;
        }
    }
}
