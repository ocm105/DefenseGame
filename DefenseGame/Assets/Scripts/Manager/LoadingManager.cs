using System.Collections;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;

public class LoadingManager : SingletonMonoBehaviour<LoadingManager>
{
    [SerializeField] GameObject fadeCanvas;
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeTime = 1f;

    [SerializeField] GameObject loadingObj;

    private Color fadeInColor = Color.clear;
    private Color fadeOutColor = Color.black;
    private bool isFade = false;

    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    #region Fade
    public void SetFadeIn(Action call = null)
    {
        LMotion.Create(fadeOutColor, fadeInColor, fadeTime)
               .WithOnComplete(() =>
               {
                   isFade = false;
                   fadeCanvas.SetActive(false);
                   fadeImage.gameObject.SetActive(false);
                   call?.Invoke();
               })
               .BindToColor(fadeImage)
               .AddTo(this);
    }
    public void SetFadeOut(Action call = null)
    {
        fadeCanvas.SetActive(true);
        fadeImage.gameObject.SetActive(true);

        LMotion.Create(fadeOutColor, fadeInColor, fadeTime)
               .WithOnComplete(() =>
               {
                   isFade = true;
                   call?.Invoke();
               })
               .BindToColor(fadeImage)
               .AddTo(this);
    }
    #endregion

    #region SceneLoad
    public async UniTask SceneLoad(string sceneName)
    {
        Debug.Log($"{sceneName} 로딩 시작");
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        Debug.Log($"{sceneName} 로딩 완료");
        // string currentSceneName = SceneManager.GetActiveScene().name;
        // SetFadeOut();
        // await UniTask.WaitUntil(() => isFade == true);

        // AsyncOperation load_op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // load_op.allowSceneActivation = false;

        // await UniTask.WaitUntil(() => load_op.progress >= 0.9f);
        // load_op.allowSceneActivation = true;
        // await UniTask.WaitUntil(() => load_op.isDone);

        // await UniTask.Yield();

        // SceneManager.UnloadSceneAsync(currentSceneName);
        // SetFadeIn();
        // await UniTask.WaitUntil(() => isFade == false);
    }
    #endregion

    #region Loading
    public void SetLoading(bool isOn)
    {
        fadeCanvas.SetActive(isOn);
        loadingObj.SetActive(isOn);
    }
    #endregion
}
