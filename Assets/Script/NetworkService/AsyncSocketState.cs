using System.Net;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// 异步SOCKET TCP 中用来存储客户端状态信息的类
/// </summary>
public class AsyncSocketState
    {
        #region 字段
        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        private byte[] recvBuffer;
        
        /// <summary>
        /// 客户端发送到服务器的报文
        /// 注意:在有些情况下报文可能只是报文的片断而不完整
        /// </summary>
        private string datagram;

        /// <summary>
        /// 客户端的Socket
        /// </summary>
        private Socket clientSocket;

        #endregion

        #region 属性

        /// <summary>
        /// 接收数据缓冲区 
        /// </summary>
        public byte[] RecvDataBuffer
        {
            get
            {
                return recvBuffer;
            }
            set
            {
                recvBuffer = value;
            }
        }
        public bool isHeartBeated = false;
    public bool isRunning = false;
        public bool isConnected {
        get {
            return clientSocket.Connected;
        }
        }
        /// <summary>
        /// 存取会话的报文
        /// </summary>
        public string Datagram
        {
            get
            {
                return datagram;
            }
            set
            {
                datagram = value;
            }
        }

        /// <summary>
        /// 获得与客户端会话关联的Socket对象
        /// </summary>
        public Socket ClientSocket
        {
            get
            {
                return clientSocket;

            }
        }
        public IPEndPoint remote;

    #endregion

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="clientSocket">会话使用的Socket连接</param>
    public AsyncSocketState(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            datagram = "";
            InitBuffer();
        }

        /// <summary>
        /// 初始化数据缓冲区
        /// </summary>
        public void InitBuffer()
        {
            if (recvBuffer == null && clientSocket != null)
            {
                recvBuffer = new byte[clientSocket.ReceiveBufferSize];
            }
        }
        public bool CheckHeartBeat() {
            if (!isHeartBeated)
            {
            LogTool.Log("失去心跳连接，断开客户端");
            return false;
            }
            else {
            isHeartBeated = false;
            isRunning = true;
            return true;
            }
        }
    /// <summary>
    /// 关闭会话
    /// </summary>
    public void Close()
    {
        //清理资源
        clientSocket.Close();
        isRunning = false;
    }
    public override bool Equals(object obj)
    {
        AsyncSocketState state = (AsyncSocketState)obj;
        return clientSocket.Equals(state);
    }
}

