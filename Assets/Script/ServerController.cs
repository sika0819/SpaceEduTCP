using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class ServerController:Singleton<ServerController>  {
    AsyncTcpServer tcpServer;
    UDPServer udpServer;
    float beatTime = 1;//每过1s检测一次心跳
    float timer = 0;
    public float ExitTime = 30;
    public int maxCount = 50;
    Thread heartBeat;
    Thread checkHeartBeat;
    public string ipAddress = "127.0.0.1";
    public int tcpPort = 10086;
    public bool isTcpStart = false;
    //public GameObject studentCopy;
    public Dictionary<string, string> clientDic;
    public Dictionary<string, StudentItem> studentDic;
    Thread connectThread;
    // Use this for initialization
    private void Awake()
    {
        SaveDataController.Init(Application.persistentDataPath, "ServerData.DATA");
        clientDic = new Dictionary<string, string>();
        studentDic = new Dictionary<string, StudentItem>();
        DontDestroyOnLoad(gameObject);
        
    }
    public void StartUdpServer()
    {
        if (udpServer == null)
        {
            udpServer = new UDPServer(2333, maxCount);
            udpServer.Start();
            udpServer.DataReceived += Broadcaster_DataReceived;
            connectThread = Loom.StartSingleThread(BroadCastConnect, System.Threading.ThreadPriority.Normal, true);
        }
    }

    public void CloseServer()
    {
        if (tcpServer != null && isTcpStart)
        {
            tcpServer.SendAll(NetworkCommand.EXIT);
            tcpServer.DataReceived -= Server_DataReceived;
            tcpServer.Dispose();
            heartBeat.Abort();
            checkHeartBeat.Abort();
            isTcpStart = false;
            tcpServer = null;
        }
        clientDic.Clear();
        if (studentDic.Count > 0)
        {
            List<string> itemList = new List<string>(studentDic.Keys);
            foreach (var item in itemList)
            {
                studentDic[item]=null;
                studentDic.Remove(item);
            }
        }
        studentDic.Clear();
    
    }
    public void CloseUdpServer()
    {

        if (udpServer != null)
        {
            udpServer.DataReceived -= Broadcaster_DataReceived;
            udpServer.Dispose();
        }
    }
    public void SendAllClient(string msg) {
        tcpServer.SendAll(msg);
    }
    private void Server_DataReceived(object sender, AsyncSocketEventArgs e)
    {
        if (tcpServer.isRunning)
        {
            LogTool.Log(e.msg);
            string remoteIpAddress = ((IPEndPoint)e.state.ClientSocket.RemoteEndPoint).Address.ToString();
            string clientName = clientDic[remoteIpAddress];

            switch (e.msg)
            {
                case NetworkCommand.HEART_BEAT:
                    List<AsyncSocketState> clientList = new List<AsyncSocketState>(tcpServer.clientDic.Values);
                    for (int i = 0; i < clientList.Count; i++)
                    {
                        if (clientList[i] != null)
                        {
                            if (e.state == clientList[i])
                            {
                                tcpServer.clientDic[clientList[i].ClientSocket.RemoteEndPoint].isHeartBeated = true;
                            }
                        }
                    }
                    break;
                case NetworkCommand.EXIT:

                    Loom.DispatchToMainThread(() =>
                    {
                        KickByName(clientName);

                    });
                    tcpServer.Close(e.state);
                    break;
                default:
                    LogTool.Log(clientDic[remoteIpAddress] + ":" + e.msg);
                    break;
            }
        }
    }
    public void KickOff()
    {
        var studentList = new List<StudentItem>(studentDic.Values);
        foreach (var item in studentList)
        {
            if (item.studentState!=null)
            {
                tcpServer.Send(item.studentState, NetworkCommand.EXIT);
                //broadcaster.sendMessage(Command.EXIT);
                DeleteStudent(item.UserName);
            }
        }

    }
    void StartServer()
    {
        if (!isTcpStart)
        {
            LogTool.Log(ipAddress + ":" + tcpPort);
            if (tcpServer == null)
            {
                tcpServer = new AsyncTcpServer(IPAddress.Parse(ipAddress), tcpPort);
                tcpServer.maxConnectCount = maxCount;
                tcpServer.Start();
                tcpServer.DataReceived += Server_DataReceived;
                heartBeat = Loom.StartSingleThread(HeartBeat, System.Threading.ThreadPriority.AboveNormal, true);
                checkHeartBeat = Loom.StartSingleThread(CheckHeatBeat, System.Threading.ThreadPriority.Normal, true);

            }
            isTcpStart = true;
        }
    }
    private void HeartBeat()
    {

        while (tcpServer.isRunning)
        {
            Loom.WaitForSeconds(beatTime);
            if (tcpServer != null)
                Loom.DispatchToMainThread(() =>
                {
                    //NGUIDebug.Log(Command.HEART_BEAT);
                    tcpServer.HeartBeat();
                }, true);
        }
    }
    private void CheckHeatBeat()
    {
        while (tcpServer.isRunning)
        {
            Loom.WaitForSeconds(ExitTime);
            if (tcpServer != null && tcpServer.clientDic != null && tcpServer.clientCount > 0)
            {
                foreach (var item in tcpServer.clientDic)
                {
                    if (item.Value.isRunning)
                    {
                        if (!item.Value.CheckHeartBeat())
                        {
                            tcpServer.Close(item.Value);

                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
    public void KickByName(string clientName)
    {
        var studentList = new List<StudentItem>(studentDic.Values);
        foreach (var item in studentList)
        {
            if (item.UserName == clientName)
            {
                DeleteStudent(item.UserName);
                //server.Send(item.studentState, Command.EXIT);
                //broadcaster.sendMessage(Command.EXIT);


            }
        }
    }
    private void DeleteStudent(string clientName)
    {
        if (studentDic.ContainsKey(clientName))
        {
            studentDic[clientName]=null;
            studentDic.Remove(clientName);
        }
    }
    private void BroadCastConnect()
    {
        if (udpServer != null)
        {
            while (udpServer.isRunning)
            {
                udpServer.sendMessage(NetworkCommand.CONNECT + ":" + tcpPort);
                Thread.Sleep(1000);
            }
        }
    }

    private void Broadcaster_DataReceived(object sender, AsyncSocketEventArgs e)
    {
        LogTool.Log(e.msg);
        //switch (e.msg) {
        //    case Command.EXIT:
        //        broadcaster.Close(e.state);
        //        break;
        //}
        if (e.msg.StartsWith(NetworkCommand.LOGIN) && clientDic.Count <= maxCount)
        {
            string[] result = e.msg.Split(';');
            LogTool.Log("Login……");
            int id = int.Parse(result[1].Split(':')[1]);
            string userName = result[2].Split(':')[1];
            string password = result[3].Split(':')[1];
            IPEndPoint remotePoint = e.state.remote;
            string remoteAddress = remotePoint.Address.ToString();

            if (!clientDic.ContainsKey(remoteAddress))
            {
                clientDic.Add(remoteAddress, userName);

            }
            Loom.DispatchToMainThread(() =>
            {
                if (!studentDic.ContainsKey(userName))
                {
                    //StudentItemView item = CreateStudent(studentCopy);
                    //item.IPAddress = remoteAddress;
                    //item.studentState = e.state;
                    //item.UserName = userName;
                    //studentDic.Add(userName, item);
                }
            });
            LogTool.Log("客户端:" + userName + "IP:" + remoteAddress);

            SaveDataController.CreateUser(id, userName, password);
            SaveDataController.SetServerPort(ipAddress, tcpPort);
            udpServer.sendMessage(NetworkCommand.ISCONNECTED);
            StartServer();
        }
    }

    private void OnApplicationQuit()
    {
        if (tcpServer != null)
            tcpServer.SendAll(NetworkCommand.EXIT);
        CloseServer();
        CloseUdpServer();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
