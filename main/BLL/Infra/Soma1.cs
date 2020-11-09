using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
  
    public static class Soma1
    {

       

        public static string GetNextNum(string numeroAnterior)
        {
            string proximoNumero = "";
            char[] arrayLetras = numeroAnterior.ToArray();
            Array.Reverse(arrayLetras);

            int numero;

            for (int i = 0; i < arrayLetras.Length; i++)
            {
                bool success = Int32.TryParse(arrayLetras[i].ToString(), out numero);

                //Soma numeros
                if (success)
                {

                    if (numero == 9)
                        arrayLetras[0] = 'A';
                    else
                        arrayLetras[i] = Convert.ToChar((numero + 1).ToString());
                    break;
                }
                else
                {
                    if (arrayLetras[i] == 'Z' && arrayLetras[0]=='9')
                    {
                        arrayLetras[i] = '0';
                        arrayLetras[0]++;
                        break;
                    }
                    arrayLetras[i]++;
                    break;
                }
            }
            Array.Reverse(arrayLetras);
            proximoNumero = new string(arrayLetras);
            return proximoNumero;
        }
    }
}

