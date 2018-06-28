using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPServer_backup : IDisposable
{
    public UdpClient udpBroadcaster;
    public event EventHandler<AsyncSocketEventArgs> DataReceived;//接收数据
    public event EventHandler<AsyncSocketEventArgs> NetError;//网络错误

    private byte[] data;
    public bool isReceived = false;
    IPAddress ipAddress = IPAddress.Any;
    private int localPort = 0;
    public bool isRunning = false;
    private IPEndPoint remoteEndPort;
    Thread broadcastThread;
    IPEndPoint sender;
    List<AsyncSocketState> clientList;
    public int ClientCount
    {
        get
        {
            return clientList.Count;
        }
    }
    int MaxCount = 0;
    int remotePort;
    public UDPServer_backup(int remotePort, int MaxCount)
    {
        this.MaxCount = MaxCount;
        udpBroadcaster = new UdpClient(AddressFamily.InterNetwork);
        ipAddress = IPAddress.Any;
        localPort = 0;
        IPEndPoint localEndPort = new IPEndPoint(ipAddress, localPort);
        //data = Encoding.Default.GetBytes(hostName);
        this.remotePort = remotePort;
        remoteEndPort = new IPEndPoint(IPAddress.Broadcast, remotePort);
        clientList = new List<AsyncSocketState>();
    }
    public void Start()
    {
        if (!isRunning)
        {
            Receive();
            isRunning = true;
        }
    }
    public void Receive()
    {
        sender = new IPEndPoint(IPAddress.Any, 0);
        if (!disposedValue)
        {
            AsyncSocketState state = new AsyncSocketState(udpBroadcaster.Client);
            udpBroadcaster.BeginReceive(DataHandler, state);
        }
    }

    private void DataHandler(IAsyncResult ar)
    {
        AsyncSocketState state = ar.AsyncState as AsyncSocketState;


        try
        {

            byte[] data = udpBroadcaster.EndReceive(ar, ref sender);
            //触发数据收到事件 
            string msg = Encoding.UTF8.GetString(data, 0, data.Length);
            state.remote = sender;
            if (!clientList.Contains(state) && clientList.Count <= MaxCount)
                clientList.Add(state);
            RaiseDataReceived(msg, state);
        }
        catch (Exception)
        {
            //TODO 处理异常  
            RaiseOtherException(state);
        }
        finally
        {
            if (isRunning && udpBroadcaster != null)
                udpBroadcaster.BeginReceive(DataHandler, state);
        }

    }

    private void RaiseDataReceived(string msg, AsyncSocketState state)
    {
        if (!disposedValue)
        {
            if (DataReceived != null)
                DataReceived.Invoke(this, new AsyncSocketEventArgs(msg, state));
        }
    }
    public void Close(AsyncSocketState state)
    {
        if (state != null && (!disposedValue))
        {
            LogTool.Log("客户端已关闭");
            state.Datagram = "";
            //TODO 触发关闭事件  
            state.Close();
        }
    }
    private void RaiseOtherException(AsyncSocketState state)
    {
        if (NetError != null)
            NetError.Invoke(this, new AsyncSocketEventArgs(state));
    }
    public void sendMessage(string msg)
    {
        if (!disposedValue && remoteEndPort != null)
        {
            LogTool.Log(msg);
            //udpBroadcaster.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            remoteEndPort = new IPEndPoint(IPAddress.Broadcast, remotePort);
            data = Encoding.UTF8.GetBytes(msg);
            udpBroadcaster.Send(data, data.Length, remoteEndPort);

        }
    }

    #region IDisposable Support
    private bool disposedValue = false; // 要检测冗余调用
    public void Close()
    {
        if (!disposedValue)
        {
            for (int i = 0; i < clientList.Count; i++)
            {
                Close(clientList[i]);
            }
            clientList.Clear();
            udpBroadcaster.Close();
            isRunning = false;
            // TODO: 释放托管状态(托管对象)。
            // broadcastThread.Abort();
            data = null;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {

            if (disposing)
            {
                Close();
            }

            // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
            // TODO: 将大型字段设置为 null。

            disposedValue = true;
        }
    }

    // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
    // ~UDPServer() {
    //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
    //   Dispose(false);
    // }

    // 添加此代码以正确实现可处置模式。
    public void Dispose()
    {
        // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        Dispose(true);
        // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
        // GC.SuppressFinalize(this);
    }
    #endregion

}
