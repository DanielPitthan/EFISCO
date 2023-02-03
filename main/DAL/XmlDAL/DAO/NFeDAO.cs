using ContextBinds.EntityCore;
using DAL.DAOBaseNfeXml;
using DAL.XmlDAL.Helpers;
using DAL.XmlDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XmlNFe.Nfes;
using XmlNFe.Nfes.Informacoes.Detalhe;

namespace DAL.XmlDAL.DAO
{
    public class NFeDAO : DataAccessBaseNfeXml, INFeDAO
    {
        public NFeDAO(ContextEFNFeXml _context) : base(_context) { }

        public async Task<NFe> CarregarXML(string xml)
        {
            NFe nfe = FuncoesXml.XmlStringParaClasse<NFe>(xml);
            nfe.infNFe.Id = nfe.infNFe.Id.Substring(3, 44);
            return nfe;
        }


        public IQueryable<NFe> GetAll()
        {
            IQueryable<NFe> notas = Contexto.NFe
                .Include(infNFeSupl => infNFeSupl.infNFeSupl)
                .Include(signature => signature.Signature)
                .Include(infNFe => infNFe.infNFe)
                .Include(inf => inf.infNFe.ide)
                .Include(inf => inf.infNFe.emit)
                   .ThenInclude(emit => emit.enderEmit)

                .Include(inf => inf.infNFe.dest)
                    .ThenInclude(dest => dest.enderDest)

                .Include(inf => inf.infNFe.retirada)
                .Include(inf => inf.infNFe.entrega)
                .Include(inf => inf.infNFe.autXML)
                .Include(inf => inf.infNFe.det)
                   .ThenInclude(det => det.imposto)
                       .ThenInclude(imp => imp.ICMS)

                .Include(inf => inf.infNFe.det)
                   .ThenInclude(det => det.imposto)
                       .ThenInclude(imp => imp.PIS)

                .Include(inf => inf.infNFe.det)
                   .ThenInclude(det => det.imposto)
                       .ThenInclude(imp => imp.COFINS)

                .Include(inf => inf.infNFe.det)
                   .ThenInclude(det => det.imposto)
                       .ThenInclude(imp => imp.IPI)

                .Include(inf => inf.infNFe.det)
                    .ThenInclude(det => det.imposto)
                        .ThenInclude(imp => imp.II)


                .Include(inf => inf.infNFe.det)
                    .ThenInclude(det => det.prod)
                // .ThenInclude(prod => prod.comb)

                .Include(inf => inf.infNFe.det)
                    .ThenInclude(det => det.impostoDevol)

                .Include(inf => inf.infNFe.total)
                    .ThenInclude(tot => tot.ICMSTot)

                .Include(inf => inf.infNFe.total)
                    .ThenInclude(tot => tot.ISSQNtot)

                .Include(inf => inf.infNFe.total)
                    .ThenInclude(tot => tot.retTrib)

                .Include(inf => inf.infNFe.transp)
                    .ThenInclude(transp => transp.transporta)

                .Include(inf => inf.infNFe.transp)
                    .ThenInclude(transp => transp.veicTransp)

                .Include(inf => inf.infNFe.transp)
                    .ThenInclude(transp => transp.vol)

                .Include(inf => inf.infNFe.transp)
                    .ThenInclude(transp => transp.retTransp)

                .Include(inf => inf.infNFe.transp)
                    .ThenInclude(transp => transp.reboque)

                .Include(inf => inf.infNFe.cobr)
                    .ThenInclude(cob => cob.fat)

                .Include(inf => inf.infNFe.pag)
                .Include(inf => inf.infNFe.infAdic)


                .AsNoTracking();

            string query = notas.ToQueryString();

            return notas;
        }

        public async Task<bool> AddAsync(NFe nfe)
        {
            await Contexto.NFe.AddAsync(nfe);
            int rowsAffecteds = await Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rowsAffecteds > 0;
        }

        public async Task<bool> UpdateAsync(NFe nfe)
        {
            NFe localContext = Contexto.Set<NFe>().Local.FirstOrDefault(entry => entry.Id.Equals(nfe.Id));
            if (localContext != null)
            {
                Contexto.Entry(localContext).State = EntityState.Detached;
                await Contexto.SaveChangesAsync().ConfigureAwait(false);
            }
            Contexto.Entry(nfe).State = EntityState.Modified;
            int rowsAffecteds = await Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rowsAffecteds > 0;
        }

        public async Task<IList<prod>> GetProduto(NFe nfe)
        {
            List<prod> produtos = await Contexto.prod.FromSqlInterpolated<prod>($@"select prod.* 
                                                                        from prod as prod with(nolock)
                                                                        join det as det with(nolock) on prod.Id = det.prodId
                                                                        join infNFe as inf with(nolock) on det.infNFeId = inf.infNFeId
                                                                        join NFe as nfe with(nolock) on inf.infNFeId = nfe.infNFeId
                                                                        where nfe.Id = {nfe.Id}")
                                                                        .AsNoTracking()
                                                                        .ToListAsync();
            return produtos;

        }

        public override bool Update<TSource>(TSource item)
        {
            return base.Update(item);
        }
    }
}
