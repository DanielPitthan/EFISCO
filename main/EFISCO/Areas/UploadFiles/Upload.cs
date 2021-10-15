using BLL.NFE.Interfaces;
using CrossCuting.Factorys;
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
        private readonly UploadFactory uploadFactory;

        //TIPO_ARQUIVO_NFE
        public Upload(IHostEnvironment _enviroment,
                     IFileStorangeDAO _fileStorange,
                     INFeXmlService _xmlService,
                     UploadFactory _uploadFactory)
        {
            enviroment = _enviroment;
            fileStorangeDAO = _fileStorange;
            nfeXmlService = _xmlService;
            uploadFactory = _uploadFactory;
            pathToSaveofTheDay = _uploadFactory.PrepareEnviroment();
        }

        [HttpPost("upload/single")]
        public async Task<JsonResult> Single(IFormFile file)
        {
            try
            {
                IFormFile[] files = new FormFile[1] { (FormFile)file};

               

                var filesStoranges = await uploadFactory.ProcessarArquivos(files, OrigemArquivo.Upload);
                FileStorange fileStorange = filesStoranges.First();


          

                return new JsonResult(new { StatusCode = StatusCode(200), fileStorange }); //StatusCode(200);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = StatusCode(500, ex.Message) });
            }
        }

        [HttpPost("upload/multiple")]
        public async Task<JsonResult> Multiple(IFormFile[] files)
        {
            if (files == null)
            {
                return new JsonResult(new { StatusCode = StatusCode(204, "Não há conteúdo para ser processado") });
            }

            try
            {

                var filesStoranges = await uploadFactory.ProcessarArquivos(files,OrigemArquivo.Upload);
                if (filesStoranges.Count > 0)
                {
                    return new JsonResult(new { StatusCode = StatusCode(200), filesStoranges });
                }
                else
                {
                    return new JsonResult(new { StatusCode = StatusCode(500, "Ocorreu um erro no processamento dos arquivos") });
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = StatusCode(500, ex.Message) });
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
                file.MD5 = uploadFactory.GetMd5FromFile(file.XmlString).ToString();
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

       

    }
}
