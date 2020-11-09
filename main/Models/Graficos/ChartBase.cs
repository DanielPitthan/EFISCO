using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Graficos
{
    public abstract class ChartBase
    {
        public  string Serie { get; set; }
        public  decimal Valor { get; set; }

        public override string ToString()
        {
            return this.Serie;
        }
    }
}
