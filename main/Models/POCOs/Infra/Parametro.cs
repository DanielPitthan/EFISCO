using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models.Infra
{
    public class Parametro : IIdentifier
    {

        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
    }
}
