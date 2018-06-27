using System;
using System.Text;
/// <summary>  
/// 异步Socket TCP事件参数类  
/// </summary>  
public class AsyncSocketEventArgs : EventArgs
{
    /// <summary>  
    /// 提示信息  
    /// </summary>  
    public string msg;

    /// <summary>  
    /// 客户端状态封装类  
    /// </summary>  
    public AsyncSocketState state;

    /// <summary>  
    /// 是否已经处理过了  
    /// </summary>  
    public bool IsHandled { get; set; }

    public AsyncSocketEventArgs(string msg)
    {
        this.msg = msg;
        IsHandled = false;
    }
    public AsyncSocketEventArgs(AsyncSocketState state)
    {
        state.Datagram = Encoding.UTF8.GetString(state.RecvDataBuffer);
        msg = state.Datagram;
        this.state = state;
        IsHandled = false;
    }
    public AsyncSocketEventArgs(string msg, AsyncSocketState state)
    {
        this.msg = msg;
        this.state = state;
        IsHandled = false;
    }
}