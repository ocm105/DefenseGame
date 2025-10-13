

// 메인화면 스테이트
public enum MainState
{
    Loading,
    Start
}

#region Setting
// 언어 타입
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
public enum GameState
{
    Start = 0,
    Pause,
    End
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
public enum SynergyType
{
    Fire,
    Water,
    Thunder,
    Wind,
    Max
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
    Monster1 = GameIndex.Monster + 1,
}
#endregion


#region Player
public enum PlayerAniState
{
    Default,
    Attack,
    Skill,
    Hit,
    Die
}
public enum PlayerAttackLevel
{
    None = 0,
    Attack1,
    Attack2,
    Attack3,
    Max
}
#endregion