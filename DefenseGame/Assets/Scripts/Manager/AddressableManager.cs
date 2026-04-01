using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UISystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : SingletonMonoBehaviour<AddressableManager>
{
    private AsyncOperationHandle<IList<GameObject>> fbxHandle;
    public List<GameObject> fbxList = new List<GameObject>();

    private AsyncOperationHandle<IList<GameObject>> popupHandle;
    public List<GameObject> popupList = new List<GameObject>();

    private AsyncOperationHandle<IList<GameObject>> viewHandle;
    public List<GameObject> viewList = new List<GameObject>();

    private AsyncOperationHandle<IList<AudioClip>> soundHandle;
    public List<AudioClip> soundList = new List<AudioClip>();

    private AsyncOperationHandle<long> sizeHandle;
    public long downSize { get; private set; }
    public float downPercent { get; private set; }
    public bool isComplete { get; private set; }


    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    #region DownLoad
    public void StartDownload_Addressable(object key, Action<AsyncOperationStatus> callback = null)
    {
        isComplete = false;
        downPercent = 0f;
        DownLoadCoroutine(key, callback).Forget();
    }

    private async UniTask DownLoadCoroutine(object key, Action<AsyncOperationStatus> callback = null)
    {
        await GetSizeAsync(key);

        // 사이즈 체크 실패 시
        if (sizeHandle.Status == AsyncOperationStatus.Failed)
        {
            DownloadFail();
            callback?.Invoke(AsyncOperationStatus.Failed);
            return;
        }

        // 다운로드 할 것이 있을 때
        if (downSize > 0)
        {
            var downloadHandle = Addressables.DownloadDependenciesAsync(key, true);

            try
            {
                var progress = Progress.Create<float>(p =>
                {
                    downPercent = p;
                    // UI 업데이트 함수를 호출
                });

                // 다운로드 완료 대기 (Progress 전달)
                await downloadHandle.ToUniTask(progress);

                if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    DownloadComplete(AsyncOperationStatus.Succeeded, callback);
                }
                else
                {
                    DownloadFail();
                    callback?.Invoke(AsyncOperationStatus.Failed);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Download Error: {e.Message}");
                DownloadFail();
            }
            finally
            {
                // 핸들 해제
                if (downloadHandle.IsValid())
                    Addressables.Release(downloadHandle);
            }
        }
        // 다운로드가 되어 있을 때
        else
        {
            DownloadComplete(AsyncOperationStatus.Succeeded, callback);
        }
    }


    // 다운받을 크기 Get
    private async UniTask GetSizeAsync(object key)
    {
        var handle = Addressables.GetDownloadSizeAsync(key);
        downSize = await handle;

        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError($"[Addressables] 크기 확인 실패: {key}");
        }
        Addressables.Release(handle);
    }

    // 다운로드 성공
    private void DownloadComplete(AsyncOperationStatus status, Action<AsyncOperationStatus> callback = null)
    {
        WindowDebug.SuccessLog("Addressable DownLoad Complete");
        downPercent = 1f;
        isComplete = true;
        callback?.Invoke(status);
    }
    // 다운로드 실패
    private void DownloadFail()
    {
        WindowDebug.FailLog("Addressable DownLoad Fail");
        // PopupState popupState = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("리소스 다운로드 실패");
        // popupState.OnOK = p => Application.Quit();
    }
    #endregion

    #region Get
    public async UniTask uniLoadData()
    {
        popupList = await GetAddressablesByLabelAsync<GameObject>("Popup");
        viewList = await GetAddressablesByLabelAsync<GameObject>("View");
        soundList = await GetAddressablesByLabelAsync<AudioClip>("Sound");
    }

    public IEnumerator LoadData()
    {
        // yield return StartCoroutine(GetAddressableFBX());
        yield return StartCoroutine(GetAddressablePopup());
        yield return StartCoroutine(GetAddressableView());
        yield return StartCoroutine(GetAddressableSound());
    }

    /// <summary>
    /// 메모리 해제 관리 필요
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    public async UniTask<List<T>> GetAddressablesByLabelAsync<T>(string label) where T : UnityEngine.Object
    {
        List<T> resultList = new List<T>();

        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, item =>
        {
            if (item != null)
            {
                resultList.Add(item);
            }
        });

        try
        {
            await handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"{label} 로드 성공.");
                return resultList;
            }
            else
            {
                Debug.LogError($"{label} 로드 실패.");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"{label} 로드 중 예외 발생: {e.Message}");
            return null;
        }
    }

    public IEnumerator GetAddressablePopup()
    {
        popupHandle = Addressables.LoadAssetsAsync<GameObject>("Popup", item =>
        {
            popupList.Add(item);
        });
        yield return new WaitUntil(() => popupHandle.IsDone);
    }
    public IEnumerator GetAddressableView()
    {
        viewHandle = Addressables.LoadAssetsAsync<GameObject>("View", item =>
        {
            viewList.Add(item);
        });
        yield return new WaitUntil(() => viewHandle.IsDone);
    }
    public IEnumerator GetAddressableSound()
    {
        soundHandle = Addressables.LoadAssetsAsync<AudioClip>("Sound", item =>
        {
            soundList.Add(item);
        });
        yield return new WaitUntil(() => soundHandle.IsDone);
    }

    public GameObject GetFBX(string key)
    {
        GameObject fbx = fbxList.Where(x => x.name == key).FirstOrDefault();
        return fbx;
    }
    public GameObject GetPopup(string key)
    {
        GameObject popup = popupList.Where(x => x.name == key).FirstOrDefault();
        return popup;
    }
    public GameObject GetView(string key)
    {
        GameObject view = viewList.Where(x => x.name == key).FirstOrDefault();
        return view;
    }
    public AudioClip GetSound(string key)
    {
        AudioClip sound = soundList.Where(x => x.name == key).FirstOrDefault();
        return sound;
    }

    public TextAsset GetTable(string key)
    {
        var table = Addressables.LoadAssetAsync<TextAsset>(key);
        return table.WaitForCompletion();
    }
    #endregion
}
