using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequestService : IHttpRequestService {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }
    //The interface demands this signal
    [Inject]
    public HttpRequestSignal fulfillSignal { get; set; }
    [Inject]
    public ILoginModel model { get; set; }
    MonoBehaviour root;
    private string url;
    public HttpRequestService()
    {
       
    }
    public void GetHttpRequest(string url, Action<bool> callback)
    {
        this.url = url;
        root = contextView.GetComponent<UIManagerRoot>();
        root.StartCoroutine(requestHttp(url, callback));
    }
    public void RequsetLogin(string url, string userName, string password, Action<bool> callback)
    {
        this.url = url;
        root = contextView.GetComponent<UIManagerRoot>();
        Dictionary<string, string> postData = new Dictionary<string, string>() { { "username", userName }, { "password", password } };
        root.StartCoroutine(requestLogin(url, postData, callback));
    }
    private IEnumerator requestLogin(string url, Dictionary<string, string> postData,Action<bool> callback)
    {
        LoginPanelView loginRoot = contextView.GetComponentInChildren<LoginPanelView>();
        using (UnityWebRequest www = UnityWebRequest.Post(CommenValue.HttpAddress+ url, postData))
        {
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                LogTool.Log(www.error);
                loginRoot.loginStateChangedSignal.Dispatch(LoginState.LoginFailed);
                if (callback != null) {
                    callback.Invoke(false);
                }
            }
            else {
                if (www.responseCode == 200)
                {
               
                    fulfillSignal.Dispatch(www.downloadHandler.text);
                    loginRoot.loginStateChangedSignal.Dispatch(LoginState.isLogined);
                    if (callback != null)
                    {
                        callback.Invoke(true);
                    }
                }
            }
        }
    }
    private IEnumerator requestHttp(string url, Action<bool> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(CommenValue.HttpAddress + url))
        {
            www.SetRequestHeader("Authorization", "Bearer " + model.token);
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                LogTool.Log(url+":"+www.error);
                if (callback != null)
                {
                    callback.Invoke(false);
                }
            }
            else
            {
                LogTool.Log(url + ":" + www.downloadHandler.text);
                if (www.responseCode == 200)
                {
                    if (callback != null)
                    {
                        callback.Invoke(true);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 获取课程缩略图
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void GetTextureRequest(string url, Action<Texture2D> callback)
    {
        root = contextView.GetComponent<UIManagerRoot>();
        root.StartCoroutine(GetTextureHttp(url, callback));
    }

    private IEnumerator GetTextureHttp(string url, Action<Texture2D> callback)
    {
        WWW www = new WWW(requestUrl.textureUrl+url);
        yield return www;
        if (www.error != null)
        {
            LogTool.Log(url + www.error);
        }
        else
        {
            if (callback != null)
            {
                callback(www.texture);
            }
        }
        www.Dispose();
    }

}
