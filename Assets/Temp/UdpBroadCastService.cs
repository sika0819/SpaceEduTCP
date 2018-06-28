using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UdpBroadCastService : ISocketService,IDisposable
{
    private EndPoint sender;
    public Socket socket { get; set; }
    public IPEndPoint localEndPoint { get; set; }
    public IPEndPoint remotePoint { get; set; }
    public bool isRunning { get; set; }
    List<AsyncSocketState> clientStateList;
    public void InitSocket(IPAddress address,int broadcastPort) {
        remotePoint = new IPEndPoint(address, broadcastPort);
        localEndPoint= new IPEndPoint(IPAddress.Any, 0);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        clientStateList = new List<AsyncSocketState>();
    }
    public void Start()
    {
        if (!isRunning)
        {
            socket.Bind(localEndPoint);
            socket.EnableBroadcast = true;
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            Receive();
            isRunning = true;
        }
    }
    public void Receive() {
        AsyncSocketState state = new AsyncSocketState(socket);
        socket.BeginReceiveFrom(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None, ref sender, new AsyncCallback(DataReceived), state);
    }
    public void SendMessage(string msg)
    {
        if (!disposedValue && remotePoint != null && socket != null)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            socket.SendTo(data, SocketFlags.None, remotePoint);
        }
    }
    public void DataReceived(IAsyncResult ar)
    {
        if (isRunning)
        {
            AsyncSocketState state = (AsyncSocketState)ar.AsyncState;
            Socket client = state.ClientSocket;

            try
            {
                //如果两次开始了异步的接收,所以当客户端退出的时候  
                //会两次执行EndReceive  
                int recv = client.EndReceiveFrom(ar, ref sender);
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
                
            }
            catch (SocketException e)
            {
                //C- TODO 异常处理  
                LogTool.Log(e.ToString());
            }
            finally
            {
                //继续接收来自来客户端的数据  
                client.BeginReceiveFrom(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None, ref sender,
                 new AsyncCallback(DataReceived), state);
            }
        }
    }

    private void Close(AsyncSocketState state)
    {
        if (state != null&&state.isRunning)
        {
            LogTool.Log("客户端已关闭");
            state.Datagram = "";
            state.isRunning = false;
            //TODO 触发关闭事件  
            state.Close();
        }
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
    // ~UdpBroadCastService() {
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

    public void Close()
    {
        socket.Close();
        isRunning = false;
        if (clientStateList != null) {
            for (int i = 0; i < clientStateList.Count; i++) {
                Close(clientStateList[i]);
            }
        }
    }
    #endregion
}
