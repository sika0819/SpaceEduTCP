using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHttpLoginService  {
    void Request(string url, string userName, string password, Action<bool> callback);
    //Instead of an EventDispatcher, we put the actual Signals into the Interface
    HttpRequestSignal fulfillSignal { get; }

}
