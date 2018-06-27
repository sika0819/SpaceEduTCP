using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class AsyncTcpClient: IDisposable
{
    public Socket tcpClient;//客户端
    public delegate void ReceivedData(string msg);
    public event ReceivedData OnReceived;
    public IPEndPoint remotePoint;//远程端口
    public IPEndPoint bindPoint;//本机端口
    private byte[] recvBuffer;
    public bool isConnected {
        private set;
        get;
    }
    public AsyncTcpClient():this(new IPEndPoint(IPAddress.Any,0)) {

    }
    public AsyncTcpClient(IPEndPoint endPoint) {
        bindPoint = endPoint;
        Start();
    }
    public void Start() {
        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//客户端
        tcpClient.Bind(bindPoint);
    }
  
    public void Connet(IPEndPoint remotePoint) {
        this.remotePoint = remotePoint;
        if (tcpClient != null)
        {
            tcpClient.BeginConnect(remotePoint, asyncResult =>
            {
                tcpClient.EndConnect(asyncResult);
                isConnected = true;
                LogTool.Log("client-->-->" + remotePoint);
                AsyncReceive();
            }, null);
        }
    }
    public void Connet() {
        Connet(remotePoint);
    }
    #region 异步接受消息
    /// <summary>
    /// 异步连接客户端回调函数
    /// </summary>
    /// <param name="tcpClient"></param>
    public void AsyncReceive()
    {
        byte[] data = new byte[1024];
        tcpClient.BeginReceive(data, 0, data.Length, SocketFlags.None, asyncResult =>
        {
            int length = tcpClient.EndReceive(asyncResult);
            if (length > 0)
            {
                string msg = Encoding.UTF8.GetString(data,0, length);

                OnDataReceive(msg);
                msg = "";
            }
            AsyncReceive();
        }, null);
    }
    #endregion
    public void OnDataReceive(string msg) {
        if(OnReceived!=null)
        OnReceived.Invoke(msg);
    }
    #region 异步发送消息
    /// <summary>
    /// 异步发送消息
    /// </summary>
    /// <param name="tcpClient">客户端套接字</param>
    /// <param name="message">发送消息</param>
    public void AsynSend(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        if (tcpClient != null)
        {
            tcpClient.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult =>
            {
            //完成发送消息
            int length = tcpClient.EndSend(asyncResult);
            //LogTool.Print("client-->-->server:"+message);
        }, null);
        }
    }
    #endregion
    /// <summary>
    /// 初始化数据缓冲区
    /// </summary>
    public void InitBuffer()
    {
        if (recvBuffer == null && tcpClient != null)
        {
            recvBuffer = new byte[tcpClient.ReceiveBufferSize];
        }
    }

    /// <summary>
    /// 关闭会话
    /// </summary>
    public void Close()
    {
        LogTool.Log("关闭客户端！");
        isConnected = false;
        recvBuffer = null;
        //清理资源
        if(tcpClient!=null)
            tcpClient.Close();
       
    }

    #region IDisposable Support
    public bool disposed = false; // 要检测冗余调用

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Close();
            }

            // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
            // TODO: 将大型字段设置为 null。

            disposed = true;
        }
    }

    // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
    // ~AsyncTcpClient() {
    //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
    //   Dispose(false);
    // }

    // 添加此代码以正确实现可处置模式。
    public void Dispose()
    {
        tcpClient = null;
        // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        Dispose(true);
        // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
        // GC.SuppressFinalize(this);
    }
    #endregion
}
