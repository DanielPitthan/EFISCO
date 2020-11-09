using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.Infra.Interfaces;
using DFe.Utils;
using Microsoft.EntityFrameworkCore;
using Models.Infra;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infra.DAO
{
    public class ParametroDAO : DataAccessBase, IParametros
    {


        public ParametroDAO(ContextEF _context) : base(_context) { }

        public DbSet<Parametro> GetAll()
        {
            return this.Contexto.Parametro;
        }
    }
}
