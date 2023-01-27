using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Radar.Entitys
{
    [Table("tb_lojas")]
    public record Loja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("nome", TypeName = "varchar(45)")]
        public string Nome { get; set; }

        [Column("observacao", TypeName = "varchar(255)")]
        public string Observacao { get; set; }

        [Required]
        [Column("cep", TypeName = "varchar(20)")]
        public string Cep { get; set; }

        [Required]
        [Column("logradouro", TypeName = "varchar(50)")]
        public string Logradouro { get; set; }

        [Required]
        [Column("numero", TypeName = "INT")]
        public int Numero { get; set; }

        [Required]
        [Column("bairro", TypeName = "varchar(45)")]
        public string Bairro { get; set; }

        [Required]
        [Column("cidade", TypeName = "varchar(45)")]
        public string Cidade { get; set; }

        [Required]
        [Column("estado", TypeName = "varchar(45)")]
        public string Estado { get; set; }

        [Column("complemento", TypeName = "varchar(45)")]
        public string Complemento { get; set; }

        [Required]
        [Column("latitude", TypeName = "DOUBLE")]
        public double latitude { get; set; }

        [Required]
        [Column("longitude", TypeName = "DOUBLE")]
        public double longitude { get; set; }


    }
}
