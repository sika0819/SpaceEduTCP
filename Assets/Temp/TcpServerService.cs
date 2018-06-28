using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TcpServerService : ISocketService
{
    public Socket socket { get; set; }
    public IPEndPoint localEndPoint { get; set; }
    public IPEndPoint remotePoint { get; set; }
    public bool isRunning { get; set; }
    public int maxCount{get;set;}
    public Dictionary<EndPoint, AsyncSocketState> clientDic;//客户端列表
    public void InitSocket(IPAddress address,int port)
    {
        localEndPoint = new IPEndPoint(address, port);
        clientDic = new Dictionary<EndPoint, AsyncSocketState>();
        socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);//建立tcp
    }
    public void Start()
    {
        if (!isRunning)
        {
            isRunning = true;
            socket.Bind(localEndPoint);
            socket.Listen(maxCount);
            socket.BeginAccept(new AsyncCallback(AyscAccept), socket);
            LogTool.Log("正在等待客户端启动");
        }
    }

    private void AyscAccept(IAsyncResult ar)
    {
        throw new NotImplementedException();
    }

    public void DataReceived(IAsyncResult ar)
    {
        throw new NotImplementedException();
    }
    public void SendMessage(string msg)
    {
        throw new NotImplementedException();
    }
    public void Close()
    {
        throw new NotImplementedException();
    }

}
