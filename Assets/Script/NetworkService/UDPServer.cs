using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPServer : IDisposable
{
    public Socket udpBroadcaster;
    public event EventHandler<AsyncSocketEventArgs> DataReceived;//接收数据
    public event EventHandler<AsyncSocketEventArgs> NetError;//网络错误
    public bool isReceived = false;
    public bool isRunning = false;
    private IPEndPoint remoteEndPort;
    Thread broadcastThread;
    EndPoint sender;

    int MaxCount = 0;
    int remotePort;
    IPEndPoint localEndPort;
    public UDPServer(int remotePort, int MaxCount)
    {
        localEndPort = new IPEndPoint(IPAddress.Any, 0);
        this.MaxCount = MaxCount;
        this.remotePort = remotePort;
        udpBroadcaster = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udpBroadcaster.Bind(localEndPort);
        udpBroadcaster.EnableBroadcast = true;
        udpBroadcaster.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
        remoteEndPort = new IPEndPoint(IPAddress.Broadcast, remotePort);
        sender = new IPEndPoint(IPAddress.Any, 0);
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
        if (!disposedValue)
        {
            LogTool.Log("begin receive");
            
            AsyncSocketState state = new AsyncSocketState(udpBroadcaster);
            udpBroadcaster.BeginReceiveFrom(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None, ref sender, new AsyncCallback(HandleDataReceived), state);
        }
    }

    private void HandleDataReceived(IAsyncResult ar)
    {
        if (isRunning)
        {
            AsyncSocketState state = (AsyncSocketState)ar.AsyncState;
            Socket client = state.ClientSocket;
            
            try
            {
                //如果两次开始了异步的接收,所以当客户端退出的时候  
                //会两次执行EndReceive  
                int recv = client.EndReceiveFrom(ar,ref sender);
                if (recv == 0)
                {
                    //C- TODO 触发事件 (关闭客户端)  
                    Close(state);
                    return;
                }
                //TODO 处理已经读取的数据 ps:数据在state的RecvDataBuffer中  
                string msg = Encoding.UTF8.GetString(state.RecvDataBuffer, 0, recv);
                state.remote = (IPEndPoint)sender;
                //C- TODO 触发数据接收事件  
                RaiseDataReceived(msg, state);
            }
            catch (SocketException e)
            {
                //C- TODO 异常处理  
                LogTool.Log(e.ToString());
            }
            finally
            {
                //继续接收来自来客户端的数据  
                client.BeginReceiveFrom(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None,ref sender,
                 new AsyncCallback(HandleDataReceived), state);
            }
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
            state.isRunning = false;
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
        if (!disposedValue&& remoteEndPort!=null&&udpBroadcaster!=null)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            udpBroadcaster.SendTo(data, SocketFlags.None, remoteEndPort);
        }
    }

    #region IDisposable Support
    private bool disposedValue = false; // 要检测冗余调用
    public void Close()
    {
        if (!disposedValue)
        {
            udpBroadcaster.Close();
            isRunning = false;
            // TODO: 释放托管状态(托管对象)。
            // broadcastThread.Abort();
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
