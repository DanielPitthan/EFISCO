using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Models.NFe.Funcoes
{
    public static class  FuncoesValor
    {
     public static decimal Arredondar(this decimal valor, int casasDecimais)
    {
        var valorNovo = decimal.Round(valor, casasDecimais, MidpointRounding.AwayFromZero);
        var valorNovoStr = valorNovo.ToString("F" + casasDecimais, CultureInfo.CurrentCulture);
        return decimal.Parse(valorNovoStr);
    }

    public static decimal? Arredondar(this decimal? valor, int casasDecimais)
    {
        if (valor == null) return null;
        return Arredondar(valor.Value, casasDecimais);
    }

    public static decimal ArredondarParaBaixo(this decimal valor, int casasDecimais)
    {
        var divisor = (decimal)Math.Pow(10, casasDecimais);
        var dividendo = (long)Math.Truncate(divisor * valor);
        return dividendo / divisor;
    }
}
}
