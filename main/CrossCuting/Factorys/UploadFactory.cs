using DAL.FileStoranges.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Models.FileStoranges;
using Models.POCOs.FileStoranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAL.Infra.Interfaces;
using DAL.Infra.DAO;
using Models.Infra;

namespace CrossCuting.Factorys
{

    public class UploadFactory
    {
        private static string pathToSaveofTheDay;
        private IHostEnvironment enviroment;
        private IFileStorangeDAO fileStorangeDAO;
       // private readonly IParametrosDAO parametroDAO;

        public UploadFactory(IHostEnvironment _enviroment,
                    IFileStorangeDAO _fileStorange)
        {
            enviroment = _enviroment;
            fileStorangeDAO = _fileStorange;
       //     this.parametroDAO = _parametroDAO;
            pathToSaveofTheDay = PrepareEnviroment();
        }

        public async Task<IList<FileStorange>> ProcessarArquivos(IFormFile[] files, OrigemArquivo origem, EmailRecebido emailRecebido = null)
        {
            
                                

            IList<FileStorange> fileStoranges = new List<FileStorange>();

            foreach (IFormFile file in files)
            {
                if (file == null)
                    continue;

                #region Monta o FileStorange
                byte[] fileByte = new byte[file.Length];
                string fileName = file.FileName;
                string extension = Path.GetExtension(fileName);
                string newFileName = $"{Guid.NewGuid()}{extension}";
                string filePath = Path.Combine(pathToSaveofTheDay, newFileName);

                FileStorange fileToStore = new FileStorange
                {
                    DataInclusao = DateTime.Now,
                    FileName = newFileName,
                    OriginalFileName = fileName,
                    Path = filePath,
                    UsuarioId = 0,
                    FileStornageUnique = Guid.NewGuid(),
                    FileType = extension,
                    Processado = false,
                    OrigemId = origem,
                    //RemetenteEmail = remetenteEmail,
                    //DataRecebimetoEmail = dataDoEmial != null && dataDoEmial.HasValue ? dataDoEmial.Value : DateTime.Now,
                    //CorpoDoEmail = bodyEmail
                    EmailRecebido = emailRecebido
                };

                using (MemoryStream mStream = new MemoryStream())
                {
                    file.CopyTo(mStream);
                    fileByte = mStream.ToArray();
                }

                fileToStore.DataByte = fileByte;

                #endregion

                try
                {
                    using (FileStream fileStrean = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {

                        await file.CopyToAsync(fileStrean);
                    }

                    if (extension.ToUpper().Equals(".XML"))
                    {
                        fileToStore.XmlString = File.ReadAllText(filePath);
                        fileToStore.MD5 = GetMd5FromFile(fileToStore.XmlString).ToString();
                        fileToStore.TipoXml = fileToStore.XmlString.Contains("nfeProc") ? "NFE" : "CTE";
                    }
                    else
                    {
                        fileToStore.MD5 = GetMd5FromFile(Encoding.UTF8.GetString(fileByte))
                                       .ToString();
                    }




                    FileStorange existeArquivo = await fileStorangeDAO.GetAll()
                                                   .Where(x => x.MD5 == fileToStore.MD5)
                                                   .FirstOrDefaultAsync();

                    if (existeArquivo != null)
                    {
                        existeArquivo.Processado = false;
                        existeArquivo.XmlString = fileToStore.XmlString;
                        existeArquivo.DataByte = fileToStore.DataByte;
                        existeArquivo.DataInclusao = DateTime.Now;
                        fileStorangeDAO.Update(existeArquivo);
                    }
                    else
                    {
                        await fileStorangeDAO.AddSysnc(fileToStore);
                    }



                    FileStorange arquivoGravado = await fileStorangeDAO.GetAll()
                                                  .Where(x => x.MD5 == fileToStore.MD5)
                                                  .FirstOrDefaultAsync();
                    if (arquivoGravado != null)
                        fileToStore.Id = arquivoGravado.Id;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                fileStoranges.Add(fileToStore);
            }


            return fileStoranges;
        }

        public StringBuilder GetMd5FromFile(string file)
        {

            MD5 md5 = MD5.Create();
            byte[] hashMd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(file));

            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < hashMd5.Length; i++)
            {
                sBuilder.Append(hashMd5[i].ToString("x2"));
            }

            return sBuilder;
        }
        public string PrepareEnviroment()
        {
            string pathToSaveofTheDay = Path.Combine(enviroment.ContentRootPath, "wwwroot", $@"Upload\XML\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}");

            if (!Directory.Exists(pathToSaveofTheDay))
            {
                Directory.CreateDirectory(pathToSaveofTheDay);
            }

            return pathToSaveofTheDay;

        }
    }
}
