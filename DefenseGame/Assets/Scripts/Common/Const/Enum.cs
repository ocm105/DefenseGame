

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
public enum UnitType
{
    Unit1 = Constants.GameIndex.Unit + 1,
    Unit2 = Constants.GameIndex.Unit + 2,
    Unit3 = Constants.GameIndex.Unit + 3,
    Unit4 = Constants.GameIndex.Unit + 4,
    // Unit5 = Constants.GameIndex.Unit + 5,
    // Unit6 = Constants.GameIndex.Unit + 6,
    // Unit7 = Constants.GameIndex.Unit + 7,
    // Unit8 = Constants.GameIndex.Unit + 8,
    // Unit9 = Constants.GameIndex.Unit + 9,
    // Unit10 = Constants.GameIndex.Unit + 10,
    // Unit11 = Constants.GameIndex.Unit + 11,
    // Unit12 = Constants.GameIndex.Unit + 12,
    // Unit13 = Constants.GameIndex.Unit + 13,
    // Unit14 = Constants.GameIndex.Unit + 14,
    // Unit15 = Constants.GameIndex.Unit + 15,
    // Unit16 = Constants.GameIndex.Unit + 16,
    // Unit17 = Constants.GameIndex.Unit + 17,
    // Unit18 = Constants.GameIndex.Unit + 18,
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
    Monster1 = Constants.GameIndex.Monster + 1,
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