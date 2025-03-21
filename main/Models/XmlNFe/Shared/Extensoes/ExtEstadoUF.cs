﻿/********************************************************************************/
/* Projeto: Biblioteca ZeusDFe                                                  */
/* Biblioteca C# para auxiliar no desenvolvimento das demais bibliotecas DFe    */
/*                                                                              */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using DFe.Classes.Entidades;
using System;
using System.Linq;

namespace DFe.Classes.Extensoes
{
    public static class ExtEstadoUF
    {
        public static Estado SiglaParaEstado(this Estado estado, string siglaUf)
        {
            Estado enumValues = Enum.GetValues(typeof(Estado)).Cast<Estado>().FirstOrDefault(e => e.GetSiglaUfString() == siglaUf);

            return enumValues;
        }

        public static Estado CodigoIbgeParaEstado(this Estado estado, string codigoIbge)
        {
            Estado enumValues = Enum.GetValues(typeof(Estado)).Cast<Estado>().FirstOrDefault(est => est.GetCodigoIbgeEmString() == codigoIbge);

            return enumValues;
        }

        public static string GetSiglaUfString(this Estado estado)
        {
            return estado.ToString();
        }

        public static string GetCodigoIbgeEmString(this Estado estado)
        {
            byte codigo = (byte)estado;
            return codigo.ToString();
        }

        public static byte GetCodigoIbgeEmByte(this Estado estado)
        {
            byte codigo = (byte)estado;

            return codigo;
        }
    }
}