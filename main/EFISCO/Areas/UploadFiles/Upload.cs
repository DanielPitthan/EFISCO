using BLL.NFE.Interfaces;
using DAL.FileStoranges.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Models.FileStoranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EFISCO.Areas.UploadFiles
{
    public class Upload : Controller
    {
        private IHostEnvironment enviroment { get; set; }
        private static string pathToSaveofTheDay;
        private readonly IFileStorangeDAO fileStorangeDAO;
        private readonly INFeXmlService nfeXmlService;

        //TIPO_ARQUIVO_NFE
        public Upload(IHostEnvironment _enviroment,
                     IFileStorangeDAO _fileStorange,
                     INFeXmlService _xmlService)
        {
            enviroment = _enviroment;
            fileStorangeDAO = _fileStorange;
            nfeXmlService = _xmlService;

            pathToSaveofTheDay = PrepareEnviroment();
        }

        [HttpPost("upload/single")]
        public async Task<JsonResult> Single(IFormFile file)
        {
            try
            {

                #region Monta o FileStorange
                byte[] fileByte = new byte[file.Length];


                FileStorange fileStorange = new FileStorange();
                string[] fileStruct = file.FileName.Split('.');

                fileStorange.FileType = fileStruct[fileStruct.Length - 1];
                fileStorange.DataInclusao = DateTime.Now;
                fileStorange.FileName = $"{Guid.NewGuid()}.{fileStruct[fileStruct.Length - 1]}";
                fileStorange.FileStornageUnique = Guid.NewGuid();
                fileStorange.Processado = false;
                fileStorange.OriginalFileName = file.FileName;

                using (MemoryStream mStream = new MemoryStream())
                {
                    file.CopyTo(mStream);
                    fileByte = mStream.ToArray();
                }

                fileStorange.MD5 = GetMd5FromFile(Encoding.UTF8.GetString(fileByte))
                                    .ToString();


                fileStorange.DataByte = fileByte;
                #endregion

                try
                {
                    FileStorange existeArquivo = await fileStorangeDAO.GetAll()
                                                    .AsNoTracking()
                                                    .Where(x => x.MD5 == fileStorange.MD5)
                                                    .SingleOrDefaultAsync();


                    if (existeArquivo != null)
                    {
                        existeArquivo.Processado = false;
                        existeArquivo.XmlString = fileStorange.XmlString;
                        existeArquivo.DataByte = fileStorange.DataByte;
                        existeArquivo.DataInclusao = DateTime.Now;
                        await fileStorangeDAO.UpdateAsync(existeArquivo);
                    }
                    else
                    {
                        await fileStorangeDAO.AddSysnc(fileStorange);

                    }

                    FileStorange arquivoGravado = await fileStorangeDAO.GetAll()
                                               .AsNoTracking()
                                               .Where(x => x.MD5 == fileStorange.MD5)
                                               .SingleOrDefaultAsync();
                    fileStorange.Id = arquivoGravado.Id;

                }
                catch (Exception ex)
                {
                    return new JsonResult(new { StatusCode = StatusCode(500, ex.Message) });// StatusCode(500, ex.Message);
                }

                return new JsonResult(new { StatusCode = StatusCode(200), fileStorange }); //StatusCode(200);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = StatusCode(500, ex.Message) });
            }
        }

        [HttpPost("upload/multiple")]
        public async Task<IActionResult> Multiple(IFormFile[] files)
        {
            if (files == null)
            {
                return StatusCode(204, "Não há conteúdo para ser processado");
            }

            try
            {
                if (await ProcessarArquivos(files))
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(500, "Ocorreu um erro no processamento dos arquivos");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        /// <summary>
        /// Faz o uploda e retorna a lista com os Ids gerados
        /// </summary>
        /// <param name="fileStoranges"></param>
        /// <returns>IList<FileStorange></returns>
        public async Task<IList<FileStorange>> ProcessarArquivos(IList<FileStorange> fileStoranges)
        {
            foreach (FileStorange file in fileStoranges)
            {

                file.DataInclusao = DateTime.Now;
                file.FileStornageUnique = Guid.NewGuid();

                file.Processado = false;

                file.XmlString = System.IO.File.ReadAllText(file.Path);
                file.MD5 = GetMd5FromFile(file.XmlString).ToString();
                try
                {
                    FileStorange existeArquivo = await fileStorangeDAO.GetAll()
                                                    .AsNoTracking()
                                                    .Where(x => x.MD5 == file.MD5)
                                                    .SingleOrDefaultAsync();


                    if (existeArquivo != null)
                    {
                        existeArquivo.Processado = false;
                        existeArquivo.XmlString = file.XmlString;
                        existeArquivo.DataInclusao = DateTime.Now;

                        await fileStorangeDAO.UpdateAsync(existeArquivo);
                        file.Id = existeArquivo.Id;
                    }
                    else
                    {
                        await fileStorangeDAO.AddSysnc(file);

                        FileStorange arquivoGravado = await fileStorangeDAO.GetAll()
                                                    .AsNoTracking()
                                                    .Where(x => x.MD5 == file.MD5)
                                                    .SingleOrDefaultAsync();
                        file.Id = arquivoGravado.Id;
                    }
                }
                catch (Exception)
                {
                    return fileStoranges;
                }



            }
            return fileStoranges;

        }

        public async Task<bool> ProcessarArquivos(IFormFile[] files)
        {

            foreach (IFormFile file in files)
            {
                ContentDispositionHeaderValue fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
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
                    Processado = false
                };

                try
                {
                    using (FileStream fileStrean = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {

                        await file.CopyToAsync(fileStrean);
                    }
                    fileToStore.XmlString = System.IO.File.ReadAllText(filePath);
                    fileToStore.MD5 = GetMd5FromFile(fileToStore.XmlString).ToString();


                    FileStorange existeArquivo = await fileStorangeDAO.GetAll()
                                                    .Where(x => x.MD5 == fileToStore.MD5)
                                                    .SingleOrDefaultAsync();

                    if (existeArquivo != null)
                    {
                        await fileStorangeDAO.UpdateAsync(existeArquivo);
                    }
                    else
                    {
                        await fileStorangeDAO.AddSysnc(fileToStore);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }

            return true;
        }

        private static StringBuilder GetMd5FromFile(string file)
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

        [HttpPost("upload/{id}")]
        public IActionResult Post(IFormFile[] files, int id)
        {
            try
            {
                // Put your code here
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private string PrepareEnviroment()
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
