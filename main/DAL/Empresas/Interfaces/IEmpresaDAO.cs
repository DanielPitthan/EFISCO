using DAL.DAOBase;
using Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Empresas.Interfaces
{
    public interface IEmpresaDAO: IDataAccessBase
    {
        IQueryable<Empresa> All();
    }
}
