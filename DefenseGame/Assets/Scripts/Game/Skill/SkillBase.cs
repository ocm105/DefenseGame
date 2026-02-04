using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class SkillBase : MonoBehaviour
{
    [SerializeField] protected Button skillBtn;

    protected bool isSkillReady = false;
    private float skillTime = -999f;
    protected float coolTime = 0f;
    protected CancellationTokenSource cancle;

    protected bool IsCoolDown => Time.time - skillTime >= coolTime;

    public virtual void Init()
    {
        skillBtn.onClick.AddListener(TrySkill);
    }

    protected void TrySkill()
    {
        if (!isSkillReady) return;
        if (!IsCoolDown) return;

        StartSkill().Forget();
    }

    protected async UniTask StartSkill()
    {
        isSkillReady = false;
        await UseSkill();
        skillTime = Time.time;
    }

    protected virtual async UniTask UseSkill() { }

    protected void CancleSkill()
    {
        if (cancle != null)
        {
            cancle.Cancel();
            cancle.Dispose();
            cancle = null;
        }
        isSkillReady = true;
    }
}
