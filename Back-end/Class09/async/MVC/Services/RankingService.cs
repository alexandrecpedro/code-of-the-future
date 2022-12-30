using MVC.Models;

namespace MVC.Services;

public class RankingService
{
    public List<Score> listaJogadores { get; set; } = new List<Score>
    {
        new Score(8, "👩‍🦰", "Marlene F. Martelli", 1298),
        new Score(1, "🧔", "Caio D. Torres", 800),
        new Score(7, "👩‍🦱", "Sandra D. Martins", 765),
        new Score(3, "👨‍🦳", "Tiago O. Vieira", 721)
    };
    private static RankingService? _instance;

    public static RankingService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RankingService();
            }

            return _instance;
        }
    }

    private RankingService() {}

    public List<Score> GetAll()
    {
        return RankingService.Instance.listaJogadores.OrderByDescending(jogador => jogador.Points).ToList();
    }

    public List<Score> Create(Score score)
    {
        GetAll().ToList().Add(score);
        return GetAll();
    }
}