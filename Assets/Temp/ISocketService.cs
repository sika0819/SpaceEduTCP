using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public interface ISocketService  {
    Socket socket { get; set; }
    IPEndPoint localEndPoint { get; set; }
    IPEndPoint remotePoint { get; set; }
    bool isRunning { get; set; }
    void InitSocket(IPAddress address,int port);
    void Start();
    void SendMessage(string msg);
    void DataReceived(IAsyncResult ar);
    void Close();
}
