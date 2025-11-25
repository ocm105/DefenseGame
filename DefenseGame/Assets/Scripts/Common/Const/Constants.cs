using System.Text;


public static class Scene
{
    public const string TitleScene = "1_TitleScene";
    public const string LobbyScene = "2_LobbyScene";
    public const string GameScene = "GameScene";
}
public static class UnitResource
{
    public const string UnitInfo = "Units/UnitInfo";
    public const string Monster = "Monsters/";
    public static string GetPrefab(string resource)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Units/");
        sb.Append(resource);
        return sb.ToString();
    }
    public static string GetMonster(string resource)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Monsters/");
        sb.Append(resource);
        return sb.ToString();
    }
}
public static class GameIndex
{
    public const int Wave = 500000;
    public const int Unit = 200000;
    public const int Monster = 100000;
    public const int Synergy = 30000;
    public const int Item = 40000;
    public const int Stage = 700000;
}
