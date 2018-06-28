using strange.extensions.command.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGetHttpCommand : Command {
    [Inject]
    public string getUrl { get; set; }
    [Inject]
    public IHttpRequestService service { get; set; }//网络请求服务

    public CallGetHttpCommand() {

    }
    public override void Execute()
    {
        Retain();

        Debug.LogWarning("CallHttpServiceCommand is Executing and received a value via Signal: " + getUrl);

        //IExampleService defines fulfillSignal as part of its API
        service.GetHttpRequest(requestUrl.courses,null);

    }
}
