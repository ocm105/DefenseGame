
public enum LobbyState
{
    None = 0,
    Character,
    Home,
    Library,
    Shop
}

#region Setting
public enum LanguageType
{
    Korean = 0,
    English,
    Japanese,
    Max,
}
#endregion

#region System
public enum GAMEDATA_STATE
{
    CONNECTDATAERROR,
    PROTOCOLERROR,
    NODATA,
    REQUESTSUCCESS
}
public enum StageType
{
    PLAINS,
    DESERT,
    BEACH,
    JUNGLE,
    SNOWYFIELD,
    DUNGEON
}
#endregion

#region Unit
public enum UnitAniState
{
    Idle,
    Attack,
    Skill,
}
public enum UnitJobType
{
    Warrior = 0,
    Ranger,
    Thief,
    Wizard
}
#endregion

#region Monster
public enum MonsterState
{
    Arive = 0,
    Stop,
    Die
}
public enum MonsterType
{
    Nomal = 0,
    Epic,
    Boss
}
#endregion
