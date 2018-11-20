using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class AsyncTcpServer:IDisposable {//异步Tcp服务器
    public Socket tcpServer;

    public event EventHandler<AsyncSocketEventArgs> DataReceived;//接收数据
    public event EventHandler<AsyncSocketEventArgs> NetError;//网络错误
    public event EventHandler<AsyncSocketEventArgs> OtherException;// 异常事件  
    public event EventHandler<AsyncSocketEventArgs> ClientConnected;
    public event EventHandler<AsyncSocketEventArgs> PrepareSend;
    public event EventHandler<AsyncSocketEventArgs> CompletedSend;

    public IPEndPoint bindPoint;//本机端口号

    public Dictionary<EndPoint,AsyncSocketState> clientDic;//客户端列表
    public int maxConnectCount = 20;
    public bool isRunning = false;
    private byte[] recvBuffer;
    private bool disposed;

    public int clientCount {
        get {
            return clientDic.Count;
        }
    }// 当前的连接的客户端数  

    public AsyncTcpServer(IPEndPoint iPEndPoint):this(iPEndPoint.Address,iPEndPoint.Port) {
       
    }
    public AsyncTcpServer(int port):this(IPAddress.Any, port) 
    {
    }
    /// <summary>  
    /// 异步Socket TCP服务器  
    /// </summary>  
    /// <param name="localIPAddress">监听的IP地址</param>  
    /// <param name="listenPort">监听的端口</param>  
    /// <param name="maxClient">最大客户端数量</param>  
    public AsyncTcpServer(IPAddress localIPAddress, int listenPort)
    {
        bindPoint = new IPEndPoint(localIPAddress, listenPort);
        clientDic = new Dictionary<EndPoint, AsyncSocketState>();
        tcpServer = new Socket(localIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);//建立tcp
        
    }
    public void Start() {
        if (!isRunning)
        {
            isRunning = true;
            tcpServer.Bind(bindPoint);
            tcpServer.Listen(maxConnectCount);
            tcpServer.BeginAccept(new AsyncCallback(AyscAccept), tcpServer);
            LogTool.Log("正在等待客户端启动");
        }
    }
    public void HeartBeat() {
        SendAll(NetworkCommand.HEART_BEAT);
    }
    /// <summary>
    /// Accept是BeginAccept的回调函数，它做了如下3件事情  
    //（1）给新的连接分配connect  
    //（2）异步接收客户端数据 
    ///（3）再次调用BeginAccept实现循环 
    /// </summary>
    /// <param name="ar">句柄</param>
    private void AyscAccept(IAsyncResult ar)
    {
        LogTool.Log(isRunning);
        if (isRunning)
        {
            
            Socket server = (Socket)ar.AsyncState;
            Socket client = server.EndAccept(ar);
            //检查是否达到最大的允许的客户端数目  
            if (clientCount >= maxConnectCount)
            {
                //C-TODO 触发事件  
                RaiseOtherException(null);
            }
            else
            {
                AsyncSocketState state = new AsyncSocketState(client);
                if (!clientDic.ContainsKey(state.ClientSocket.RemoteEndPoint))
                {
                    LogTool.Log("新增客户端:" + state.ClientSocket.RemoteEndPoint);
                    clientDic.Add(state.ClientSocket.RemoteEndPoint,state);
                   
                }
                RaiseClientConnected(state); //触发客户端连接事件  
                state.InitBuffer();
                //开始接受来自该客户端的数据  
                client.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(HandleDataReceived), state);
            }
            //接受下一个请求  
            server.BeginAccept(new AsyncCallback(AyscAccept), ar.AsyncState);
        }
    }
    /// <summary>  
    /// 发送数据  
    /// </summary>  
    /// <param name="state">接收数据的客户端会话</param>  
    /// <param name="data">数据报文</param>  
    public void Send(AsyncSocketState state, string message)
    {
        //NGUIDebug.Log(message);
        if (state.ClientSocket.Connected)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            RaisePrepareSend(state);
            Send(state.ClientSocket, data);
        }
    }
    public void SendAll(string msg) {
        LogTool.Log("Send:" + msg);
        if (clientCount > 0)
        {
            List<AsyncSocketState> clientList = new List<AsyncSocketState>(clientDic.Values);
            for (int item =0;item< clientList.Count;item++)
            {
                Send(clientList[item], msg);
            }
        }
    }
    private void RaisePrepareSend(AsyncSocketState state)
    {
        if (PrepareSend != null)
        {
            PrepareSend(this, new AsyncSocketEventArgs(state));
        }
    }

    /// <summary>  
    /// 异步发送数据至指定的客户端  
    /// </summary>  
    /// <param name="client">客户端</param>  
    /// <param name="data">报文</param>  
    public void Send(Socket client, byte[] data)
    {
        if (!isRunning)
            throw new InvalidProgramException("This TCP Scoket server has not been started.");

        if (client == null)
            throw new ArgumentNullException("client");

        if (data == null)
            throw new ArgumentNullException("data");
        if (client != null)
        {
            client.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), client);
        }
    }

    /// <summary>  
    /// 发送数据完成处理函数  
    /// </summary>  
    /// <param name="ar">目标客户端Socket</param>  
    private void SendDataEnd(IAsyncResult ar)
    {
        ((Socket)ar.AsyncState).EndSend(ar);
        RaiseCompletedSend(null);
    }
    /// <summary>  
    /// 触发数据发送完毕的事件  
    /// </summary>  
    /// <param name="state"></param>  
    private void RaiseCompletedSend(AsyncSocketState state)
    {
        if (CompletedSend != null)
        {
            CompletedSend(this, new AsyncSocketEventArgs(state));
        }
    }
    /// <summary>  
    /// 触发客户端连接事件  
    /// </summary>  
    /// <param name="state"></param>  
    private void RaiseClientConnected(AsyncSocketState state)
    {
        if (ClientConnected != null)
        {
            ClientConnected(this, new AsyncSocketEventArgs(state));
        }
    }

    /// <summary>  
    /// 处理客户端数据  
    /// </summary>  
    /// <param name="ar"></param>  
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
                int recv = client.EndReceive(ar);
                if (recv == 0)
                {
                    //C- TODO 触发事件 (关闭客户端)  
                    Close(state);
                    RaiseNetError(state);
                    return;
                }
                //TODO 处理已经读取的数据 ps:数据在state的RecvDataBuffer中  
                string msg = Encoding.UTF8.GetString(state.RecvDataBuffer,0, recv);

                //C- TODO 触发数据接收事件  
                RaiseDataReceived(msg,state);
            }
            catch (SocketException)
            {
                //C- TODO 异常处理  
                RaiseNetError(state);
            }
            finally
            {
                //继续接收来自来客户端的数据  
                client.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(HandleDataReceived), state);
            }
        }
    }

    /// <summary>  
    /// 触发网络错误事件  
    /// </summary>  
    /// <param name="state"></param>  
    private void RaiseNetError(AsyncSocketState state)
    {
        if (NetError != null)
        {
            NetError(this, new AsyncSocketEventArgs(state));
        }
    }

    private void RaiseDataReceived(string msg,AsyncSocketState state)
    {
        if (DataReceived != null)
        {
            DataReceived(this, new AsyncSocketEventArgs(msg,state));
        }
    }
    /// <summary>  
    /// 触发异常事件  
    /// </summary>  
    /// <param name="state"></param>  
    private void RaiseOtherException(AsyncSocketState state, string descrip)
    {
        if (OtherException != null)
        {
            OtherException(this, new AsyncSocketEventArgs(descrip, state));
        }
    }
    private void RaiseOtherException(AsyncSocketState state)
    {
        RaiseOtherException(state, "");
    }

    public void Close(AsyncSocketState state)
    {
        if (state != null&&!disposed)
        {
            if (clientDic.ContainsKey(state.ClientSocket.RemoteEndPoint))
                clientDic.Remove(state.ClientSocket.RemoteEndPoint);
            state.Datagram = null;
            state.RecvDataBuffer = null;
            //TODO 触发关闭事件  
            state.Close();
            
        }
    }
    public void CloseAllClient() {
        List<AsyncSocketState> client = new List<AsyncSocketState>(clientDic.Values);
        foreach (var item in client) {
            Close(item);
        }
        clientDic.Clear();
        clientDic = null;
    }

    #region 释放  
    /// <summary>  
    /// Performs application-defined tasks associated with freeing,   
    /// releasing, or resetting unmanaged resources.  
    /// </summary>  
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>  
    /// Releases unmanaged and - optionally - managed resources  
    /// </summary>  
    /// <param name="disposing"><c>true</c> to release   
    /// both managed and unmanaged resources; <c>false</c>   
    /// to release only unmanaged resources.</param>  
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                try
                {
                    Stop();
                    
                    LogTool.Log("退出服务器");
                }
                catch (SocketException)
                {
                    //TODO  
                    RaiseOtherException(null);
                }
            }
            disposed = true;
        }
    }
    #endregion
    /// <summary>  
    /// 停止服务器  
    /// </summary>  
    public void Stop()
    {
        if (isRunning)
        {
            isRunning = false;
            CloseAllClient();
            tcpServer.Close();
            isRunning = false;
            //TODO 关闭对所有客户端的连接  
        }
    }

}
