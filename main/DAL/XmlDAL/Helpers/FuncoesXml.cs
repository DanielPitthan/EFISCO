﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL.XmlDAL.Helpers
{
    public static class FuncoesXml
    {

        // https://github.com/ZeusAutomacao/DFe.NET/issues/610
        private static readonly Hashtable CacheSerializers = new Hashtable();

        /// <summary>
        ///     Serializa a classe passada para uma string no form
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static string ClasseParaXmlString<T>(T objeto)
        {
            XElement xml;
            string keyNomeClasseEmUso = typeof(T).FullName;
            XmlSerializer ser = BuscarNoCache(keyNomeClasseEmUso, typeof(T));

            using (MemoryStream memory = new MemoryStream())
            {
                using (TextReader tr = new StreamReader(memory, Encoding.UTF8))
                {
                    ser.Serialize(memory, objeto);
                    memory.Position = 0;
                    xml = XElement.Load(tr);
                    xml.Attributes().Where(x => x.Name.LocalName.Equals("xsd") || x.Name.LocalName.Equals("xsi")).Remove();
                }
            }
            return XElement.Parse(xml.ToString()).ToString(SaveOptions.DisableFormatting);
        }

        /// <summary>
        ///     Deserializa a classe a partir de uma String contendo a estrutura XML daquela classe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T XmlStringParaClasse<T>(string input) where T : class
        {
            string keyNomeClasseEmUso = typeof(T).FullName;
            XmlSerializer ser = BuscarNoCache(keyNomeClasseEmUso, typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        /// <summary>
        ///     Carrega o objeto da classe com dados do arquivo XML (Deserializa a classe). Atenção o XML deve ter a mesma
        ///     estrutura da classe
        /// </summary>
        /// <typeparam name="T">Classe</typeparam>
        /// <param name="arquivo">Arquivo XML</param>
        /// <returns>Retorna a classe</returns>
        public static T ArquivoXmlParaClasse<T>(string arquivo) where T : class
        {
            if (!File.Exists(arquivo))
            {
                throw new FileNotFoundException("Arquivo " + arquivo + " não encontrado!");
            }

            string keyNomeClasseEmUso = typeof(T).FullName;
            XmlSerializer serializador = BuscarNoCache(keyNomeClasseEmUso, typeof(T));
            FileStream stream = new FileStream(arquivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                return (T)serializador.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        ///     Copia a estrutura e os dados da classe passada para um arquivo XML (Serializa a classe). Use try catch para tratar
        ///     a possível exceção "DirectoryNotFoundException"
        /// </summary>
        /// <typeparam name="T">Classe</typeparam>
        /// <param name="objeto">Objeto da Classe</param>
        /// <param name="arquivo">Arquivo XML</param>
        public static void ClasseParaArquivoXml<T>(T objeto, string arquivo)
        {
            string dir = Path.GetDirectoryName(arquivo);
            if (dir != null && !Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException("Diretório " + dir + " não encontrado!");
            }

            string xml = ClasseParaXmlString(objeto);
            try
            {
                StreamWriter stw = new StreamWriter(arquivo);
                stw.WriteLine(xml);
                stw.Close();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível criar o arquivo " + arquivo + "!");
            }
        }

        public static void SalvarStringXmlParaArquivoXml(string xml, string arquivo)
        {
            string dir = Path.GetDirectoryName(arquivo);
            if (dir != null && !Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException("Diretório " + dir + " não encontrado!");
            }

            try
            {
                StreamWriter stw = new StreamWriter(arquivo);
                stw.WriteLine(xml);
                stw.Close();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível criar o arquivo " + arquivo + "!");
            }
        }

        /// <summary>
        ///     Obtém um node XML no formato string de um arquivo XML. Util por exemplo, para extrair uma NFe de um XML contendo um
        ///     nfeproc, enviNFe, etc.
        /// </summary>
        /// <param name="nomeDoNode"></param>
        /// <param name="stream"></param>
        /// <returns>Retorna a string contendo o node XML cujo strem foi passado no parâmetro nomeDoNode</returns>
        public static string ObterNodeDeStream(string nomeDoNode, StreamReader stream)
        {
            XDocument xmlDoc = XDocument.Load(stream);

            XElement xmlString = (from d in xmlDoc.Descendants()
                                  where d.Name.LocalName == nomeDoNode
                                  select d).FirstOrDefault();

            if (xmlString == null)
            {
                throw new Exception(string.Format("Nenhum objeto {0} encontrado no stream!", nomeDoNode));
            }

            return xmlString.ToString();
        }

        /// <summary>
        ///     Obtém um node XML no formato string de um arquivo XML. Util por exemplo, para extrair uma NFe de um XML contendo um
        ///     nfeproc, enviNFe, etc.
        /// </summary>
        /// <param name="nomeDoNode"></param>
        /// <param name="arquivoXml"></param>
        /// <returns>Retorna a string contendo o node XML cujo nome foi passado no parâmetro nomeDoNode</returns>
        public static string ObterNodeDeArquivoXml(string nomeDoNode, string arquivoXml)
        {
            XDocument xmlDoc = XDocument.Load(arquivoXml);
            XElement xmlString = (from d in xmlDoc.Descendants()
                                  where d.Name.LocalName == nomeDoNode
                                  select d).FirstOrDefault();

            if (xmlString == null)
            {
                throw new Exception(string.Format("Nenhum objeto {0} encontrado no arquivo {1}!", nomeDoNode, arquivoXml));
            }

            return xmlString.ToString();
        }

        /// <summary>
        ///     Obtém um node XML no formato string de um arquivo XML. Util por exemplo, para extrair uma NFe de um XML contendo um
        ///     nfeproc, enviNFe, etc.
        /// </summary>
        /// <param name="nomeDoNode"></param>
        /// <param name="stringXml"></param>
        /// <returns>Retorna a string contendo o node XML cujo nome foi passado no parâmetro nomeDoNode</returns>
        public static string ObterNodeDeStringXml(string nomeDoNode, string stringXml)
        {
            string s = stringXml;
            XDocument xmlDoc = XDocument.Parse(s);
            XElement xmlString = (from d in xmlDoc.Descendants()
                                  where d.Name.LocalName == nomeDoNode
                                  select d).FirstOrDefault();

            if (xmlString == null)
            {
                throw new Exception(string.Format("Nenhum objeto {0} encontrado no xml!", nomeDoNode));
            }

            return xmlString.ToString();
        }

        // https://github.com/ZeusAutomacao/DFe.NET/issues/610
        private static XmlSerializer BuscarNoCache(string chave, Type type)
        {
            try
            {
                if (CacheSerializers.Contains(chave))
                {
                    return (XmlSerializer)CacheSerializers[chave];
                }


                XmlSerializer ser = XmlSerializer.FromTypes(new[] { type })[0];
                CacheSerializers.Add(chave, ser);
                return ser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

}
