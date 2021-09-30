using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCuting.Tools
{
    public class EmailControl
    {

        private string host = "";
        private string user = "";
        private string password = "";

        public EmailControl()
        {

        }

        public void Ler()
        {
            using (Imap imap = new Imap())
            {
                imap.ConnectSSL("outlook.office365.com");
                imap.UseBestLogin("nfe@colorobbia.com.br", "Colorobbia123");

                
                imap.Select("NFeMotor");
                
                List<long> uids = imap.Search(Flag.All);

                foreach (long uid in uids)
                {
                    IMail email = new MailBuilder()
                        .CreateFromEml(imap.GetMessageByUID(uid));

                    Console.WriteLine(email.Subject);
                    Console.WriteLine(uid);

                    // save all attachments to disk
                    foreach (MimeData mime in email.Attachments)
                    {
                        mime.Save($"C:\\TEMP\\{mime.SafeFileName}");
                    }
                    imap.MoveByUID(uid, "NFeMotor_Lido");
                }

                
                imap.Close();
            }
        }
    }
}
