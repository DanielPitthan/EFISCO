using DAL.DAOBaseNfeXml;
using Microsoft.EntityFrameworkCore;
using Models.FileStoranges;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.FileStoranges.Interfaces
{
    public interface IFileStorangeDAO:IDataAccessBaseNfeXml
    {
        DbSet<FileStorange> GetAll();

    }
}
