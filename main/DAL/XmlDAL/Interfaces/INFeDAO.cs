using DAL.DAOBaseNfeXml;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XmlNFe.Nfes;
using XmlNFe.Nfes.Informacoes.Detalhe;

namespace DAL.XmlDAL.Interfaces
{
    public interface INFeDAO
    {
        /// <summary>
        /// Carrga um arquivo Xml e transforme em um objeto NFe
        /// </summary>
        /// <param name="FullPath"></param>        
        /// <returns>NFe</returns>
        Task<NFe> CarregarXML(string xml);

        IQueryable<NFe> GetAll();

        Task<bool> AddAsync(NFe nfe);
        Task<bool> UpdateAsync(NFe nfe);

        Task<IList<prod>> GetProduto(NFe nfe);
        
    }
}
