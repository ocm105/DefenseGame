using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;

public class DamageFont : UIUtil
{
    [SerializeField] TMP_Text text;

    public async UniTask Intialize(float damage, Transform pos)
    {
        text.text = damage.ToString();
        SetPosition(pos.position);
        SetActive(true);

        await LMotion.Create(0f, 10f, 0.5f).BindToLocalPositionY(text.transform).AddTo(this);
        End();
    }

    public override void SetPosition(Vector3 target)
    {
        base.SetPosition(target);
        float pos = this.transform.position.y;
    }

    private void End()
    {
        SetActive(false);
        text.transform.localPosition = Vector3.zero;
    }
}
