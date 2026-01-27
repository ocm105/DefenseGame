using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using LitMotion;

public class WaringPanel : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image icon;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text timeText;

    public void SetInfo(Sprite icon, int wave, float wavetime)
    {
        canvasGroup.alpha = 0f;
        this.icon.sprite = icon;
        waveText.text = $"wave {wave.ToString()}";
        timeText.text = GetTime(wavetime);

        StartWaringTween().Forget();
    }

    private string GetTime(float time)
    {
        var m = time / 60f;
        var s = time % 60f;

        string t;
        if (m < 1) m = 0;
        t = $"{m:00}:{s:00}";
        return t;
    }

    private async UniTask StartWaringTween()
    {
        this.gameObject.SetActive(true);
        await LMotion.Create(0f, 1f, 0.5f).Bind(a => canvasGroup.alpha = a).AddTo(this);
        await UniTask.WaitForSeconds(3f);
        await LMotion.Create(1f, 0f, 0.5f).Bind(a => canvasGroup.alpha = a).AddTo(this);
        this.gameObject.SetActive(false);
    }

}
