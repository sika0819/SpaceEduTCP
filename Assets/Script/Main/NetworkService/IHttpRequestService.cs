using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHttpRequestService  {
    void RequsetLogin(string url, string userName, string password);
    void GetHttpRequest(string url);
    void GetTextureRequest(string url);
    //Instead of an EventDispatcher, we put the actual Signals into the Interface
    HttpRequestSignal fulfillSignal { get; set; }
    GenerateCourseListSignal generateCourseListSignal { get; set; }
    ReceiveIconSignal receiveIconSignal { get; set; }
}
