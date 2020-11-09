using ContextBinds.EntityCore;
using DAL.DAOBaseNfeXml;
using DAL.FileStoranges.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.FileStoranges;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.FileStoranges.DAO
{
    public class FileStorangeDAO : DataAccessBaseNfeXml, IFileStorangeDAO
    {
        public FileStorangeDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public DbSet<FileStorange> GetAll()
        {
            return this.Contexto.FileStorange;
        }
    }
}
