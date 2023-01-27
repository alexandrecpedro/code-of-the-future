using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Radar.Entitys
{
    [Table("tb_clientes")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("nome", TypeName = "varchar(50)")]
        public string Nome { get; set; }

        [Column("telefone", TypeName = "varchar(20)")]
        public string Telefone { get; set; }

        [Required]
        [Column("email", TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column("cpf", TypeName = "varchar(45)")]
        public string Cpf { get; set; }

        [Required]
        [Column("cep", TypeName = "varchar(45)")]
        public string Cep { get; set; }

        [Required]
        [Column("logradouro", TypeName = "varchar(45)")]
        public string Logradouro { get; set; }

        [Required]
        [Column("numero", TypeName = "DOUBLE")]
        public int Numero { get; set; }

        [Required]
        [Column("bairro", TypeName = "varchar(45)")]
        public string Bairro { get; set; }

        [Required]
        [Column("cidade", TypeName = "varchar(45)")]
        public string Cidade { get; set; }

        [Required]
        [Column("estado", TypeName = "varchar(15)")]
        public string Estado { get; set; }


        [Column("complemento", TypeName = "varchar(45)")]
        public string Complemento { get; set; }

    }
}
