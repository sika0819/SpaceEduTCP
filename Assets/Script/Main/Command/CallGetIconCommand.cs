using System;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class CallGetIconCommand : Command {
    [Inject]
    public CourseRequest requestData { get; set; }
    [Inject]
    public IHttpRequestService httpRequestService { get; set; }

    public CallGetIconCommand() {
    }
    public override void Execute()
    {
        Retain();
        httpRequestService.GetTextureRequest(requestData.textureUrl, requestData.thumb);
    }

}
