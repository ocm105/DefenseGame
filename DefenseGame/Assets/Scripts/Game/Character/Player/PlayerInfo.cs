using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public Camera _mainCamera { get { return mainCamera; } }

    [Header("기본 Joystick 이동")]
    [SerializeField] PlayerMoveControl playerMoveControl;
    public PlayerMoveControl _playerMoveControl { get { return playerMoveControl; } }

    [SerializeField] PlayerAniControl playerAniControl;
    public PlayerAniControl _playerAniControl { get { return playerAniControl; } }



    [SerializeField] // 테스트
    private GameObject player;
    public GameObject _player { get { return player; } }
    // public PlayerData playerData { get; private set; }

    private void Awake()
    {

    }
    private void Start()
    {
        CreatePlayer();
    }

    // Player 생성 및 초기 상태
    private void CreatePlayer()
    {
        // playerMoveControl.SetPlayerSpeed(3f);//GameDataManager.Instance.player_Data[0].speed);
        playerAniControl.AnimationChanger(PlayerAniState.Default);
        playerAniControl.SetMove(false);
    }

}
