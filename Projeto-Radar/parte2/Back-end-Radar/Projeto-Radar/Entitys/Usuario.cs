using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Radar.Entitys
{
    [Table("tb_usuarios")]
    public record Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("nome", TypeName = "varchar(45)")]
        public string Nome { get; set; }

        [Required]
        [Column("email", TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column("senha", TypeName = "varchar(50)")]
        public string Senha { get; set; }

        [Required]
        [Column("permissao", TypeName = "varchar(15)")]
        public string Permissao { get; set; }

    }
}
