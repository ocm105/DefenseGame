using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniControl : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Animator animator;
    private bool isAttack = false;
    private bool isCombo = false;
    private bool isHit = false;
    private bool isDie = false;
    public PlayerAttackLevel playerAttackLevel;
    public PlayerAniState playerAniState { get; private set; }

    private void Awake()
    {
        playerInfo = this.GetComponentInParent<PlayerInfo>();
        animator = this.GetComponent<Animator>();
    }

    // private void Start()
    // {
    //     SetAttack_Level(PlayerAttackLevel.None);
    // }


    #region Move
    public void SetMove(bool value)
    {
        if (isHit || IsDie()) return;

        animator.SetBool("Move", value);
    }
    #endregion

    #region Attack
    public void Attack()
    {
        if (isHit || IsDie()) return;

        SetAttack(true);

        if (isCombo == false)
            StartCoroutine(ComboCoroutin());

        AnimationChanger(PlayerAniState.Attack);
    }
    public IEnumerator ComboCoroutin()
    {
        yield return new WaitUntil(() => !isCombo);

        SetAttack_Level(playerAttackLevel + 1);
        yield break;
    }

    public void AniEvent_EndAttack()
    {
        if (isCombo) return;

        SetAttack(false);
        AnimationChanger(PlayerAniState.Default);
    }
    public void AniEvent_EnableCombo()
    {
        isCombo = true;
    }
    public void AniEvent_DisableCombo()
    {
        isCombo = false;
    }
    #endregion

    #region Hit
    public void Hit()
    {
        if (IsDie()) return;

        AnimationChanger(PlayerAniState.Hit);
    }
    public void AniEvent_Hit(bool isOn)
    {
        isHit = isOn;
    }
    #endregion

    public void Die()
    {
        AnimationChanger(PlayerAniState.Die);
    }

    private IEnumerator AnimationCoroutine(string key)
    {
        if (playerAniState == PlayerAniState.Default)
        {
            animator.CrossFade(key.ToString(), 0.1f, 0);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(key));
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);

            AnimationChanger(PlayerAniState.Default);
        }
        yield break;
    }

    #region Function
    public void AnimationChanger(PlayerAniState state)
    {
        if (!IsDie())
        {
            switch (state)
            {
                case PlayerAniState.Default:
                    animator.CrossFade("Idle", 0.1f, 0);
                    SetAttack_Level(PlayerAttackLevel.None);
                    break;
                case PlayerAniState.Attack:

                    break;
                case PlayerAniState.Skill:

                    break;
                case PlayerAniState.Hit:
                    animator.CrossFade("Hit", 0.1f, 0);
                    break;
                case PlayerAniState.Die:
                    isDie = true;
                    animator.CrossFade("Die", 0.1f, 0);
                    break;
            }
        }

        playerAniState = state;
    }

    private void SetAttack(bool isOn)
    {
        isAttack = isOn;
        animator.SetBool("Attack", isAttack);
    }
    private void SetAttack_Level(PlayerAttackLevel level)
    {
        if (level >= PlayerAttackLevel.Max)
            level = PlayerAttackLevel.Attack1;

        playerAttackLevel = level;
        animator.SetInteger("Attack_Level", (int)level);
    }

    private bool IsDie()
    {
        return isDie;
    }
    #endregion

}
