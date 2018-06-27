using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpLoginService : IHttpLoginService {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }
    //The interface demands this signal
    [Inject]
    public HttpRequestSignal fulfillSignal { get; set; }
    
    private string url;
    public HttpLoginService()
    {
    }
    public void Request(string url, string userName, string password, Action<bool> callback)
    {
        this.url = url;
        
        Dictionary<string, string> postData = new Dictionary<string, string>() { { "username", userName }, { "password", password } };
        MonoBehaviour root = contextView.GetComponent<UIManagerRoot>();
        root.StartCoroutine(requestHttp(url, postData, callback));
    }
    private IEnumerator requestHttp(string url, Dictionary<string, string> postData,Action<bool> callback)
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
}
