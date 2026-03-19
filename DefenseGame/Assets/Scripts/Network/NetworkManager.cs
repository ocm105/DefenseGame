using System;
using Cysharp.Threading.Tasks;
using UISystem;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class NetworkRequestData
{
    public GAMEDATA_STATE state;
    public string data;

    public NetworkRequestData(GAMEDATA_STATE state, string data)
    {
        this.state = state;
        this.data = data;
    }
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    private bool NetworkCheck()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            return true;
        }
        else
        {
            return true;
        }
    }

    public async UniTask<NetworkRequestData> Request_Get(string url)
    {
        if (NetworkCheck())
        {
            NetworkRequestData data;

            UnityWebRequest request = UnityWebRequest.Get(url);

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    data = new NetworkRequestData(GAMEDATA_STATE.CONNECTDATAERROR, request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    data = new NetworkRequestData(GAMEDATA_STATE.PROTOCOLERROR, request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.Success:
                    data = new NetworkRequestData(GAMEDATA_STATE.REQUESTSUCCESS, request.downloadHandler.text);
                    break;
                default:
                    data = null;
                    break;
            }

            request.Dispose();

            return data;
        }
        return null;
    }

    public async UniTask<NetworkRequestData> Request_Post(string url, WWWForm form)
    {
        if (NetworkCheck())
        {
            NetworkRequestData data;

            UnityWebRequest request = UnityWebRequest.Post(url, form);
            await request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    data = new NetworkRequestData(GAMEDATA_STATE.CONNECTDATAERROR, request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    data = new NetworkRequestData(GAMEDATA_STATE.PROTOCOLERROR, request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.Success:
                    data = new NetworkRequestData(GAMEDATA_STATE.REQUESTSUCCESS, request.downloadHandler.text);
                    break;
                default:
                    data = null;
                    break;
            }

            request.Dispose();

            return data;
        }
        return null;
    }

}
