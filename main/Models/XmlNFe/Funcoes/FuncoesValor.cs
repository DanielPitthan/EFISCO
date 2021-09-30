using System;
using System.Globalization;

namespace Models.NFe.Funcoes
{
    public static class FuncoesValor
    {
        public static decimal Arredondar(this decimal valor, int casasDecimais)
        {
            decimal valorNovo = decimal.Round(valor, casasDecimais, MidpointRounding.AwayFromZero);
            string valorNovoStr = valorNovo.ToString("F" + casasDecimais, CultureInfo.CurrentCulture);
            return decimal.Parse(valorNovoStr);
        }

        public static decimal? Arredondar(this decimal? valor, int casasDecimais)
        {
            if (valor == null)
            {
                return null;
            }

            return Arredondar(valor.Value, casasDecimais);
        }

        public static decimal ArredondarParaBaixo(this decimal valor, int casasDecimais)
        {
            decimal divisor = (decimal)Math.Pow(10, casasDecimais);
            long dividendo = (long)Math.Truncate(divisor * valor);
            return dividendo / divisor;
        }
    }
}
