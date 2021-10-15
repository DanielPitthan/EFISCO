using CrossCuting.Factorys;
using DAL.Infra.Interfaces;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using Microsoft.AspNetCore.Http;

using Models.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCuting.Tools
{
    public class EmailControl
    {
        private readonly IParametrosDAO parametroDAO;
        private readonly UploadFactory uploadFactory;
        private Parametro HostEmailNfe;
        private Parametro UsuarioDoEmailNfe;
        private Parametro SenhaEmailDoNFe;
        private Parametro PastaEmail_Ler;
        private Parametro PastaEmailNfe_Lido;

        public EmailControl(IParametrosDAO _paramDAO,
                            UploadFactory _uploadFactory)
        {
            this.parametroDAO = _paramDAO;
            this.uploadFactory = _uploadFactory;


            HostEmailNfe = SetParametro("HostEmailNfe");
            UsuarioDoEmailNfe = SetParametro("UsuarioDoEmailNfe");
            SenhaEmailDoNFe = SetParametro("SenhaEmailDoNFe");
            PastaEmail_Ler = SetParametro("PastaEmail_Ler");
            PastaEmailNfe_Lido = SetParametro("PastaEmailNfe_Lido");
        }

        /// <summary>
        /// Faz o set dos parâmetros
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        private Parametro SetParametro(string codigo)
        {
            var param = parametroDAO.GetAll()
                .Where(c => c.Codigo.Equals(codigo))
                .FirstOrDefault();
            return param;
        }

        public int EmailsPendentes()
        {
            using (Imap imap = new Imap())
            {
                try
                {
                    imap.ConnectSSL(HostEmailNfe.Valor);
                    imap.UseBestLogin(UsuarioDoEmailNfe.Valor, SenhaEmailDoNFe.Valor);

                    try
                    {
                        imap.Select(PastaEmail_Ler.Valor);
                    }
                    catch (Exception ex)
                    {
                        imap.SelectInbox();
                    }
                    List<long> uids = imap.Search(Flag.Unseen);

                    return uids.Count;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public async Task<bool> Ler()
        {
            using (Imap imap = new Imap())
            {
                try
                {
                    imap.ConnectSSL(HostEmailNfe.Valor);
                    imap.UseBestLogin(UsuarioDoEmailNfe.Valor, SenhaEmailDoNFe.Valor);

                    try
                    {
                        imap.Select(PastaEmail_Ler.Valor);
                    }
                    catch (Exception ex)
                    {
                        imap.SelectInbox();
                    }


                    List<long> uids = imap.Search(Flag.Unseen);


                    IFormFile[] arquivosAProcessar = new FormFile[1]; //Processar um arquivo por vez

                    foreach (long uid in uids)
                    {
                        IMail email = new MailBuilder()
                            .CreateFromEml(imap.GetMessageByUID(uid));


                        string from = "";
                        for (int i = 0; i < email.From.Count; i++)
                        {
                            from += email.From[i].Address;
                        }

                        //if (email.Date.HasValue && email.Date <= DateTime.Now.AddYears(-1))
                        //{
                        //    continue;
                        //}
                        string bodyEmail = email.GetBodyAsText();

                        if (!string.IsNullOrWhiteSpace(bodyEmail))
                            if (bodyEmail.Length < 80)
                                bodyEmail = bodyEmail.Substring(0, bodyEmail.Length);
                            else
                                bodyEmail = bodyEmail.Substring(0, 80);



                        //Gera lista de anexos a Processar 
                        foreach (MimeData mime in email.Attachments)
                        {
                            if (mime.ContentType.MimeType.Name.Equals("message") || mime.ContentType.MimeType.Name.Equals("image"))
                            {
                                continue;
                            }
                            var extension = Path.GetExtension(mime.FileName).ToUpper();

                            if (extension.Equals(".XML"))
                            {
                                using (MemoryStream mbStrem = new MemoryStream())
                                {

                                    mime.Save(mbStrem);
                                    arquivosAProcessar[0] = new FormFile(mbStrem, 0, mbStrem.Length, mime.FileName, mime.FileName);

                                    var arquivosProcessados = await uploadFactory.ProcessarArquivos(arquivosAProcessar, Models.FileStoranges.OrigemArquivo.Email, from, email.Date, bodyEmail);
                                }
                            }
                        }

                        //try
                        //{
                        //    //Move os e-mail para outra pasta
                        //    imap.MoveByUID(uid, PastaEmailNfe_Lido.Valor);
                        //}
                        //catch (Exception ex)
                        //{
                        //    continue;

                        //}

                    }

                    imap.Close();
                    //  return arquivosProcessados.Count > 0;
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }


            }
        }
    }
}
