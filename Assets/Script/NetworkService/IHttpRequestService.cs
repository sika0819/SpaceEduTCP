using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHttpRequestService  {
    void RequsetLogin(string url, string userName, string password, Action<bool> callback);
    void GetHttpRequest(string url, Action<bool> callback);
    //Instead of an EventDispatcher, we put the actual Signals into the Interface
    HttpRequestSignal fulfillSignal { get; set; }


}
