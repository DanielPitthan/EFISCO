using Limilabs.Client.IMAP;
using Limilabs.Client.POP3;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using System;
using System.Collections.Generic;

namespace EFISCO.Email.Teste
{
    class Program
    {
        static void Main(string[] args)
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
