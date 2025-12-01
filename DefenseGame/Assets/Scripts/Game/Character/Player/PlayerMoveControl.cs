using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveControl : MonoBehaviour
{
    //     #region 이동
    //     private Vector3 originPosition; // 초기 위치
    //     private Vector3 movePostion;
    //     private Vector3 lookPosition;
    //     private float winX, winZ;   // 윈도우 컨트롤 X,Z 값
    //     private float mobX, mobZ;   // 모바일 컨트롤 X,Z 값
    //     private float moveValue = 0f;   // 조이스틱 이동 값
    //     private float playerSpeed = 0f; // 기본 스피드
    //     public void SetPlayerSpeed(float speed) { playerSpeed = speed; }
    //     private Joystick joystick;
    //     public void SetJoystick(Joystick joystick) { this.joystick = joystick; }
    //     #endregion

    //     private Camera mainCam;
    //     private PlayerInfo playerInfo;
    //     private CharacterController characterController;

    //     private void Awake()
    //     {
    //         playerInfo = this.GetComponentInParent<PlayerInfo>();
    //         characterController = this.GetComponent<CharacterController>();
    //         mainCam = playerInfo._mainCamera;
    //         originPosition = this.transform.position;
    //     }

    //     private void Init()
    //     {
    //         this.transform.position = originPosition;
    //         characterController.velocity.Set(0f, 0f, 0f);

    //         playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Default);
    //         playerInfo._playerAniControl.SetMove(false);
    //     }

    //     private void FixedUpdate()
    //     {
    //         if (joystick == null)
    //             return;

    //         switch (playerInfo._playerAniControl.playerAniState)
    //         {
    //             case PlayerAniState.Default:
    //                 mobX = Mathf.Abs(joystick.Horizontal) >= 0.05 ? joystick.Horizontal : 0;
    //                 mobZ = Mathf.Abs(joystick.Vertical) >= 0.05 ? joystick.Vertical : 0;

    //                 winX = Input.GetAxis("Horizontal");
    //                 winZ = Input.GetAxis("Vertical");


    //                 movePostion.x = mobX;
    //                 movePostion.z = mobZ;
    // #if UNITY_EDITOR_WIN
    //                 movePostion.x += winX;
    //                 movePostion.z += winZ;
    // #endif
    //                 moveValue = Mathf.Clamp01(Mathf.Abs(movePostion.x) + Mathf.Abs(movePostion.z));

    //                 if (moveValue > 0)
    //                 {
    //                     lookPosition = Quaternion.LookRotation(movePostion).eulerAngles;
    //                     this.transform.rotation = Quaternion.Euler(Vector3.up * (lookPosition.y + mainCam.transform.eulerAngles.y)).normalized;
    //                     characterController.Move(this.transform.forward * playerSpeed * moveValue * Time.fixedDeltaTime);
    //                     playerInfo._playerAniControl.SetMove(true);
    //                 }
    //                 else
    //                     playerInfo._playerAniControl.SetMove(false);

    //                 break;
    //         }
    //     }
}
