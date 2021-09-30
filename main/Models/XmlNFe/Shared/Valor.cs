using System.Globalization;

namespace DFe.Classes
{
    public static class Valor
    {
        public static decimal Arredondar(this decimal valor, int casasDecimais)
        {
            decimal valorNovo = decimal.Round(valor, casasDecimais);
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
    }
}
