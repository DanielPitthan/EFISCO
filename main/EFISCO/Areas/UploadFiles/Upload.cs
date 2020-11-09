using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BLL.NFE.Interfaces;
using DAL.FileStoranges.Interfaces;
using DFe.Utils;
using MatBlazor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Models.FileStoranges;

namespace EFISCO.Areas.UploadFiles
{
    public class Upload : Controller
    {
        private IHostEnvironment enviroment { get; set; }
        private static string pathToSaveofTheDay;
        private IFileStorangeDAO fileStorangeDAO;
        private INFeXmlService nfeXmlService;

        //TIPO_ARQUIVO_NFE
        public Upload(IHostEnvironment _enviroment,
                     IFileStorangeDAO _fileStorange,
                     INFeXmlService _xmlService)
        {
            this.enviroment = _enviroment;
            this.fileStorangeDAO = _fileStorange;
            this.nfeXmlService = _xmlService;

            pathToSaveofTheDay = PrepareEnviroment();
        }

        [HttpPost("upload/single")]
        public IActionResult Single(IFormFile file)
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

        [HttpPost("upload/multiple")]
        public async Task<IActionResult> Multiple(IFormFile[] files)
        {
            if (files == null)
                return StatusCode(204, "Não há conteúdo para ser processado");
            
            try
            {
                if (await ProcessarArquivos(files))
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(500,"Ocorreu um erro no processamento dos arquivos");
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
            foreach (var file in fileStoranges)
            {

                file.DataInclusao = DateTime.Now;
                file.FileStornageUnique = Guid.NewGuid();

                file.Processado = false;

                file.XmlString = System.IO.File.ReadAllText(file.Path);
                file.MD5 = GetMd5FromFile(file.XmlString).ToString();
                try
                {
                    var existeArquivo = await this.fileStorangeDAO.GetAll()
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

                        var arquivoGravado = await this.fileStorangeDAO.GetAll()
                                                    .Where(x => x.MD5 == file.MD5)
                                                    .SingleOrDefaultAsync();
                        file.Id = arquivoGravado.Id;
                    }
                }
                catch (Exception ex)
                {
                    return fileStoranges;
                }



            }
            return fileStoranges;

        }

            public async Task<bool> ProcessarArquivos(IFormFile[] files)
        {

            foreach (var file in files)
            {
                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                string fileName = file.FileName;
                string extension = Path.GetExtension(fileName);
                string newFileName = $"{Guid.NewGuid()}{extension}";
                string filePath = Path.Combine(pathToSaveofTheDay, newFileName);

                FileStorange fileToStore = new FileStorange();
                fileToStore.DataInclusao = DateTime.Now;
                fileToStore.FileName = newFileName;
                fileToStore.OriginalFileName = fileName;
                fileToStore.Path = filePath;
                fileToStore.UsuarioId = 0;
                fileToStore.FileStornageUnique = Guid.NewGuid();
                fileToStore.FileType = extension;
                fileToStore.Processado = false;

                try
                {
                    using (var fileStrean = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {

                        await file.CopyToAsync(fileStrean);
                    }
                    fileToStore.XmlString = System.IO.File.ReadAllText(filePath);
                    fileToStore.MD5 = GetMd5FromFile(fileToStore.XmlString).ToString();


                    var existeArquivo = await this.fileStorangeDAO.GetAll()
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
                    return false;
                }
            }

            return true;
        }

        private static StringBuilder GetMd5FromFile(string file)
        {
            
           var md5 = MD5.Create();
            var hashMd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(file));

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
                Directory.CreateDirectory(pathToSaveofTheDay);

            return pathToSaveofTheDay;

        }

    }
}
