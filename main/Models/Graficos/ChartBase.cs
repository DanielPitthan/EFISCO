namespace Models.Graficos
{
    public abstract class ChartBase
    {
        public string Serie { get; set; }
        public decimal Valor { get; set; }

        public override string ToString()
        {
            return Serie;
        }
    }
}
