using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPClient:IDisposable  {
    Socket udpClient;
    public event EventHandler<AsyncSocketEventArgs> DataReceived;//接收数据
    public event EventHandler<AsyncSocketEventArgs> NetError;//网络错误
    public bool IsRunning = false;
    IPEndPoint localPoint;
    EndPoint sender;
    public UDPClient(int port) {
        udpClient = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        localPoint = new IPEndPoint(IPAddress.Any, port);
        sender = new IPEndPoint(IPAddress.Any, 0);
    }

    

    public void Start() {
        if (!IsRunning)
        {
            IsRunning = true;
            udpClient.Bind(localPoint);
            AsyncSocketState state = new AsyncSocketState(udpClient);
            udpClient.BeginReceiveFrom(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None,ref sender, new AsyncCallback(DataHandler), state);
            
        }
    }

    /// <summary>
    /// 异步发送消息
    /// </summary>
    /// <param name="tcpClient">客户端套接字</param>
    /// <param name="message">发送消息</param>
    public void AsynSend(string message)
    {
        LogTool.Log("Send:" + message);
        byte[] data = Encoding.UTF8.GetBytes(message);
        if (udpClient != null && sender!=null&&!disposedValue)
        {
            udpClient.BeginSendTo(data, 0, data.Length, SocketFlags.None,sender, asyncResult =>
            {
                //完成发送消息
                int length = udpClient.EndSendTo(asyncResult);
            }, null);
        }
    }
    private void DataHandler(IAsyncResult ar)
    {
        AsyncSocketState state = (AsyncSocketState)ar.AsyncState;
        Socket client = state.ClientSocket;
        int len = -1;
        try
        {
            len = udpClient.EndReceiveFrom(ar,ref sender);
            state.remote = (IPEndPoint)sender; 
            //TODO 处理数据  
            string msg = Encoding.UTF8.GetString(state.RecvDataBuffer, 0, len);
           
            RaiseDataReceived(msg, state);
        }
        catch (Exception)
        {
            //TODO 处理异常  
            RaiseOtherException(state);
        }
        finally
        {
            if (IsRunning && udpClient != null)
                client.BeginReceiveFrom(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None,ref sender, new AsyncCallback(DataHandler), state);
        }

    }

    private void RaiseDataReceived(string msg, AsyncSocketState state)
    {
        if (!disposedValue && state.isConnected)
        {
            if(DataReceived!=null)
            DataReceived.Invoke(this, new AsyncSocketEventArgs(msg, state));
        }
    }

    private void RaiseOtherException(AsyncSocketState state)
    {
        if(NetError!=null)
        NetError.Invoke(this, new AsyncSocketEventArgs(state));
    }


    private bool disposedValue = false; // 要检测冗余调用

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)。
                udpClient.Close();

            }

            // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
            // TODO: 将大型字段设置为 null。

            disposedValue = true;
        }
    }

    // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
    // ~UDPClient() {
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

}
