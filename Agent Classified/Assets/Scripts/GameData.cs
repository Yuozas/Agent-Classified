public sealed class GameData
{
    public ScoreHandler ScoreHandler { get; set; }
    public MonetaryHandler MonetaryHandler { get; set; }
    private static readonly GameData instance = new GameData();

    static GameData()
    {
    }

    private GameData()
    {
    }

    public static GameData Instance => instance;
}