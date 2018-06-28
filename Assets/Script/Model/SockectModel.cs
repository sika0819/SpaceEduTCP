using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
[Serializable]
public class SockectDataModel : ISockectDataModel
{
    public string UserName { get; set; }
    public string Message{get;set;}
    public Vector3 FocusPoint { get; set; }
    public IPEndPoint LocalEndPoint{get;set;}
}
