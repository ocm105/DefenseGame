using System.Text;


public static class Scene
{
    public const string Title = "1.TitleScene";
    public const string GoogleAds = "2.GoogleAdsScene";
    public const string UnityAds = "3.UnityAdsScene";
    public const string Localization = "4.LocalizatoinScene";
    public const string CameraView = "5.CameraScene";
    public const string PlayerMove = "6.PlayerMoveScene";
    public const string WebView = "7.WebviewScene";
    public const string DongleGame = "8.DongleGameScene";
}
public static class UnitResource
{
    public const string UnitInfo = "Units/UnitInfo";
    public const string Monster = "Monsters/";
    public static string GetPrefab(int lv, string str)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Units/Unit1");
        return sb.ToString();
    }
    public static string GetImage()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Units/Unit1");
        return sb.ToString();
    }
    public static string GetMonster(string str)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Monsters/");
        sb.Append(str);
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
