using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageFont : UIUtil
{
    [SerializeField] TMP_Text text;
    
    public void Intialize(float damage, Transform pos)
    {
        text.text = damage.ToString();
        SetPosition(pos.position);
        SetActive(true);
    }

    public override void SetPosition(Vector3 target)
    {
        base.SetPosition(target);
        float pos = this.transform.position.y;
        text.transform.DOLocalMoveY(10f, 0.5f).OnComplete(End);
    }

    private void End()
    {
        SetActive(false);
        text.transform.localPosition = Vector3.zero;
    }
}
