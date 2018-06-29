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
    public ILoginModel loginModel { get; set; }
    [Inject]
    public GenerateCourseListSignal generateCourseListSignal { get; set; }
    [Inject]
    public ReceiveIconSignal receiveIconSignal { get; set; }

    MonoBehaviour root;
    private string url;
    public HttpRequestService()
    {
       
    }
    public void GetHttpRequest(string url)
    {
        this.url = url;
        root = contextView.GetComponent<UIManagerRoot>();
        root.StartCoroutine(requestHttp(url));
    }
    public void RequsetLogin(string url, string userName, string password)
    {
        this.url = url;
        root = contextView.GetComponent<UIManagerRoot>();
        Dictionary<string, string> postData = new Dictionary<string, string>() { { "username", userName }, { "password", password } };
        root.StartCoroutine(requestLogin(url, postData));
    }
    private IEnumerator requestLogin(string url, Dictionary<string, string> postData)
    {
        LoginPanelView loginRoot = contextView.GetComponentInChildren<LoginPanelView>();
        using (UnityWebRequest www = UnityWebRequest.Post(CommenValue.HttpAddress+ url, postData))
        {
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                LogTool.Log(www.error);
                loginRoot.loginStateChangedSignal.Dispatch(LoginState.LoginFailed);
            }
            else {
                if (www.responseCode == 200)
                {
               
                    fulfillSignal.Dispatch(www.downloadHandler.text);
                    loginRoot.loginStateChangedSignal.Dispatch(LoginState.isLogined);
                }
            }
        }
    }
    private IEnumerator requestHttp(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(CommenValue.HttpAddress + url))
        {
            www.SetRequestHeader("Authorization", "Bearer " + loginModel.token);
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                LogTool.Log(url+":"+www.error);
          
            }
            else
            {
                if (www.responseCode == 200)
                {
                    switch (url)
                    {
                        case requestUrl.courses:
                            var data = JsonUtility.FromJson<CourseList>(www.downloadHandler.text);
                            generateCourseListSignal.Dispatch(data);
                            break;
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
    public void GetTextureRequest(string url)
    {
        root = contextView.GetComponent<UIManagerRoot>();
        root.StartCoroutine(GetTextureHttp(url));
    }

    private IEnumerator GetTextureHttp(string url)
    {
        WWW www = new WWW(requestUrl.textureUrl+url);
        yield return www;
        if (www.error != null)
        {
            LogTool.Log(url + www.error);
        }
        else
        {
            LogTool.Log(url);
            Texture2D texture2D = www.texture;
            texture2D.name = url;
            receiveIconSignal.Dispatch(texture2D);
        }
        www.Dispose();
    }

}
