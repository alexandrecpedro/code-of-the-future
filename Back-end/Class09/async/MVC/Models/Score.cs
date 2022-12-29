using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Score
{
    public Score() { }
    public Score(int id, string? avatar, string playerName, int points)
    {
        Id = id;
        Avatar = avatar;
        PlayerName = playerName;
        Points = points;
    }

    public int Id { get; set; }
    public string? Avatar { get; set; }
    
    [Required(ErrorMessage = "Nome do jogador é obrigatório")]
    [Display(Name = "Nome do Jogador")]
    public string PlayerName { get; set; }
    [Required(ErrorMessage = "A Pontuação é obrigatória")]
    [Range(1, 999999, ErrorMessage = "Pontuação deve estar entre 1 e 999999")]
    public int Points { get; set; }
}
