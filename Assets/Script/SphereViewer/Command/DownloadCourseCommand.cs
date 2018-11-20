using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DownloadCourseCommand : Command {
    [Inject]
    public string courseID { get; set; }
    [Inject]
    public IHttpRequestService service { get; set; }//网络请求服务
    [Inject]
    public CallHttpGetIconSignal getIconSignal { get; set; }//下载图像
    [Inject]
    public SetTopicListSignal setTopicListSignal { get; set; }
    public string assetPath;
    
    public DownloadCourseCommand()
    {

        
    }
    public override void Execute()
    {
        Retain();
       
        assetPath = CommenValue.AssetPath + CommenValue.ImagePath;
        LogTool.Log(assetPath);
        if (!Directory.Exists(assetPath))
        {
            Directory.CreateDirectory(assetPath);
        }

        service.GetHttpRequest(requestUrl.courses, courseID, requestUrl.topics, textureCallback);
        setTopicListSignal.Dispatch();
    }
    public void textureCallback(string[] backTextureurl) {
        for(int i = 0; i < backTextureurl.Length; i++)
        {
           
            if (!File.Exists(CommenValue.AssetPath + backTextureurl[i]))
            {
                CourseRequest courseRequest = new CourseRequest(requestUrl.textureUrl, backTextureurl[i]);
                getIconSignal.Dispatch(courseRequest);
            }
            else {
                Debug.Log("file:///" + CommenValue.AssetPath+backTextureurl[i]);
                CourseRequest courseRequest = new CourseRequest("file:///"+CommenValue.AssetPath, backTextureurl[i]);
                getIconSignal.Dispatch(courseRequest);
            }
        }
    }
    
    public override void Release()
    {
        //base.Release();
      
    }
}
