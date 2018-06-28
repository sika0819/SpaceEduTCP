using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UdpClientService : ISocketService,IDisposable
{
    public Socket socket { get; set; }

    public IPEndPoint localEndPoint { get; set; }
    public IPEndPoint remotePoint { get; set; }
    public bool isRunning { get; set; }
    public EndPoint sender;
    public void InitSocket(IPAddress address, int localPort)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        localEndPoint = new IPEndPoint(address, localPort);
        
    }
    public void Start()
    {
        if (!isRunning)
        {
            isRunning = true;
            socket.Bind(localEndPoint);
            AsyncSocketState state = new AsyncSocketState(socket);
            socket.BeginReceiveFrom(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None, ref sender, new AsyncCallback(DataReceived), state);

        }
    }

    public void DataReceived(IAsyncResult ar)
    {
        AsyncSocketState state = (AsyncSocketState)ar.AsyncState;
        Socket client = state.ClientSocket;
        int len = -1;
        try
        {
            len = socket.EndReceiveFrom(ar, ref sender);
            state.remote = (IPEndPoint)sender;
            //TODO 处理数据  
            string msg = Encoding.UTF8.GetString(state.RecvDataBuffer, 0, len);
        }
        catch (Exception e)
        {
            //TODO 处理异常  
            LogTool.Log(e);
        }
        finally
        {
            if (isRunning && socket != null)
                client.BeginReceiveFrom(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None, ref sender, new AsyncCallback(DataReceived), state);
        }
    }

    public void SendMessage(string msg)
    {
        LogTool.Log("Send:" + msg);
        byte[] data = Encoding.UTF8.GetBytes(msg);
        if (socket != null && sender != null)
        {
            socket.BeginSendTo(data, 0, data.Length, SocketFlags.None, sender, asyncResult =>
            {
                //完成发送消息
                int length = socket.EndSendTo(asyncResult);
            }, null);
        }
    }
    public void Close()
    {
        socket.Close();
    }

    #region IDisposable Support
    private bool disposedValue = false; // 要检测冗余调用

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)。
                Close();
            }

            // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
            // TODO: 将大型字段设置为 null。

            disposedValue = true;
        }
    }

    // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
    // ~UdpClientService() {
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
