using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    [Inject]
    public ITopicList topicList { get; set; }
    MonoBehaviour root;
    private string url;
    public HttpRequestService()
    {
       
    }
    public void GetHttpRequest(string url)
    {
        this.url = url;
        root = contextView.GetComponent<MonoBehaviour>();
        root.StartCoroutine(requestHttp(url));
    }
    public void GetHttpRequest(string url, string id,string childPath, Action<string[]> textureCallback)
    {
        this.url = url;
        root = contextView.GetComponent<MonoBehaviour>();
        root.StartCoroutine(requestTopic(url,id,childPath,textureCallback));
    }
    public void RequsetLogin(string url, string userName, string password)
    {
        this.url = url;
        root = contextView.GetComponent<MonoBehaviour>();
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
    private IEnumerator requestTopic(string url,string id,string child,Action<string[]> textureCallback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(CommenValue.HttpAddress + url+"/"+id+"/"+child))
        {
            LogTool.Log(CommenValue.HttpAddress+url + "/" + id);
            www.SetRequestHeader("Authorization", "Bearer " + loginModel.token);
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                LogTool.Log(url + ":" + www.error);

            }
            else
            {
                if (www.responseCode == 200&&www.downloadHandler.text!= "Not authorized")
                {
                    LogTool.Log(www.downloadHandler.text);
                    var data = JsonHelper.getJsonArray<Topic>(www.downloadHandler.text);
                    topicList.topics = data;
                    string[] result = new string[data.Length];
                    for (int i = 0; i < data.Length; i++) {
                        Debug.Log(data[i].asset.url);
                        result[i] = data[i].asset.url;
                        
                    }
                    textureCallback.Invoke(result);
                }
            }
        }
    }
    /// <summary>
    /// 获取课程缩略图
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void GetTextureRequest(string url, string textureName)
    {
        root = contextView.AddComponent<Downloader>();
        root.StartCoroutine(GetTextureHttp(url,textureName));
    }
    
    private IEnumerator GetTextureHttp(string url, string textureName)
    {
        WWW www= new WWW(url+textureName);
        if (www != null)
        {
            
            //float progress = www.bytesDownloaded / www.bytes.Length;
            //progressSignal.Dispatch(progress);
            yield return www;
            //Debug.Log(www.bytesDownloaded + "," + www.bytes.Length);
            if (www.error != null)
            {
                LogTool.Log(url + ":" + www.error);
            }
            else if (www.isDone)
            {
                LogTool.Log(textureName);
                Texture downloadTex = www.texture;
                downloadTex.name = textureName;
                receiveIconSignal.Dispatch(downloadTex);
                //www.Dispose();
            }
        }
    }

    
}
