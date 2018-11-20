using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCommand : Command {
    [Inject]
    public DownloadCourseSignal downloadCourseSignal { get; set; }
    [Inject]
    public ILoginModel loginModel { get; set; }
    public string id { get; set; }
    public InitCommand() {

        
        
    }
    public override void Execute()
    {
        Retain();
        id = PlayerPrefs.GetString(GlobalName.CourseId);
        loginModel.token = PlayerPrefs.GetString("token");
        GameObject go = GameObject.Find("UIRoot").gameObject;
        go.AddComponent<UIMangerView>();
        downloadCourseSignal.Dispatch(id);
    }
}
