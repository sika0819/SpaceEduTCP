using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ServerTest : MonoBehaviour {
    AsyncTcpServer server;
    UDPServer broadcaster;
    UDPClient client;
    float beatTime=1;//每过1s检测一次心跳
    float timer=0;
    public float ExitTime = 30;
    public int maxCount=50;
    Thread heartBeat;
    Thread checkHeartBeat;
    public string ipAddress = "127.0.0.1";
    public int tcpPort=10086;
    public UILabel logText;
    public bool isServerStart = false;
    public bool isTcpStart = false;
    public GameObject studentCopy;
    public Dictionary<string, string> clientDic;
    public Dictionary<string, StudentItemView> studentDic;
    Thread connectThread;
    // Use this for initialization
    void Start () {

        SaveDataController.Init(Application.persistentDataPath, "ServerData.DATA");
        clientDic = new Dictionary<string, string>();
        studentDic = new Dictionary<string, StudentItemView>();
        Application.quitting += Application_quitting;
        ipAddress = Network.player.ipAddress;
    }
    public void OnToggleServer(UILabel buttonText) {
        isServerStart = !isServerStart;
        if (isServerStart)
        {
            broadcaster = new UDPServer(2333, maxCount);
            broadcaster.Start();
            client = new UDPClient(2333);
            broadcaster.DataReceived += Broadcaster_DataReceived;
            connectThread = Loom.StartSingleThread(BroadCastConnect, System.Threading.ThreadPriority.Normal, true);
            buttonText.text = "停止监听";
        }
        else {
           
            buttonText.text = "开始监听";
            CloseServer();
            client.Dispose();
        }
    }
    void StartServer() {
        if (!isTcpStart)
        {
            LogTool.Log(ipAddress + ":" + tcpPort);
            if (server == null)
            {
                server = new AsyncTcpServer(IPAddress.Parse(ipAddress), tcpPort);
                server.maxConnectCount = maxCount;
                server.Start();
                server.DataReceived += Server_DataReceived;
                heartBeat = Loom.StartSingleThread(HeartBeat, System.Threading.ThreadPriority.AboveNormal, true);
                checkHeartBeat = Loom.StartSingleThread(CheckHeatBeat, System.Threading.ThreadPriority.Normal, true);

            }
            isTcpStart = false;
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
                    StudentItemView item = CreateStudent(studentCopy);
                    item.IPAddress = remoteAddress;
                    item.studentState = e.state;
                    item.UserName = userName;
                    studentDic.Add(userName, item);
                }
            });
            LogTool.Log("客户端:" + userName + "IP:" + remoteAddress);
            
            SaveDataController.CreateUser(id, userName, password);
            SaveDataController.SetServerPort(ipAddress, tcpPort);
            broadcaster.sendMessage(NetworkCommand.ISCONNECTED);
            StartServer();
        }
        
    }
    public StudentItemView CreateStudent(GameObject go) {
        StudentItemView item = go.AddComponent<StudentItemView>();
        item.InitItemView(go);
        return item;
    }
    private void Update()
    {
   
    }
 
    private void HeartBeat()
    {

        while (server.isRunning)
        {
            Loom.WaitForSeconds(beatTime);
            if (server != null)
                Loom.DispatchToMainThread(() =>
                {
                    //NGUIDebug.Log(Command.HEART_BEAT);
                    server.HeartBeat();
                },true);
        }
    }
    private void CheckHeatBeat() {
        while (server.isRunning)
        {
            Loom.WaitForSeconds(ExitTime);
            if (server != null &&server.clientDic != null&& server.clientCount > 0)
            {
                foreach (var item in server.clientDic)
                {
                    if (item.Value.isRunning)
                    {
                        if (!item.Value.CheckHeartBeat())
                        {
                            server.Close(item.Value);

                        }
                    }
                }
            }
            else {
                return;
            }
        }
    }
    private void Server_DataReceived(object sender, AsyncSocketEventArgs e)
    {
        if (server.isRunning)
        {
            LogTool.Log(e.msg);
            string remoteIpAddress = ((IPEndPoint)e.state.ClientSocket.RemoteEndPoint).Address.ToString();
            string clientName = clientDic[remoteIpAddress];
            
            switch (e.msg)
            {
                case NetworkCommand.HEART_BEAT:
                    List<AsyncSocketState> clientList = new List<AsyncSocketState>(server.clientDic.Values);
                    for (int i = 0; i < clientList.Count; i++)
                    {
                        if (clientList[i] != null)
                        {
                            if (e.state == clientList[i])
                            {
                                server.clientDic[clientList[i].ClientSocket.RemoteEndPoint].isHeartBeated = true;
                            }
                        }
                    }
                    break;
                case NetworkCommand.EXIT:
                    
                    Loom.DispatchToMainThread(() =>
                    {
                        KickByName(clientName);
                        
                    });
                    server.Close(e.state);
                    break;
                default:
                    LogTool.Log(clientDic[remoteIpAddress]+":"+e.msg);
                    break;
            }
        }
    }
    public void KickOff() {
        var studentList = new List<StudentItemView>(studentDic.Values);
        foreach (var item in studentList)
        {
            if (item.isChecked)
            {
                server.Send(item.studentState, NetworkCommand.EXIT);
                //broadcaster.sendMessage(Command.EXIT);
                DeleteStudent(item.UserName);
            }
        }
        
    }
    public void KickByName(string clientName) {
        var studentList = new List<StudentItemView>(studentDic.Values);
        foreach (var item in studentList)
        {
            if (item.UserName==clientName)
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
            studentDic[clientName].Destory();
            studentDic.Remove(clientName);
        }
    }

    public void TcpSend(UIInput input) {
        if (server != null)
        {
            server.SendAll(input.value);
            input.value = "";
        }
    }
    // Update is called once per frame
    void FixedUpdate () {

    }
    void BroadCastConnect() {
        if (broadcaster != null)
        {
            while (broadcaster.isRunning)
            {
                broadcaster.sendMessage(NetworkCommand.CONNECT + ":" + tcpPort);
                Thread.Sleep(1000);
            }
        }
    }
    void closeBroadcaster() {
        
        if (broadcaster != null)
        {
            broadcaster.sendMessage(NetworkCommand.EXIT);
            broadcaster.DataReceived -= Broadcaster_DataReceived;
            broadcaster.Dispose();
        }
    }
    public void CloseServer() {
        
        if (server != null&&isTcpStart)
        {
            server.SendAll(NetworkCommand.EXIT);
            server.DataReceived -= Server_DataReceived;
            server.Dispose();
            heartBeat.Abort();
            checkHeartBeat.Abort();
            isTcpStart = false;
            server = null;
        }
        clientDic.Clear();
        if (studentDic.Count > 0) {
            List<string> itemList = new List<string>(studentDic.Keys);
           foreach(var item in itemList)
           {
                studentDic[item].Destory();
                studentDic.Remove(item);
           }
        }
        studentDic.Clear();
        closeBroadcaster();
       
    }
    public void Quit() {
        
        Application.Quit();
    }

    private void Application_quitting()
    {
        if(server!=null)
        server.SendAll(NetworkCommand.EXIT);
        if(broadcaster!=null)
        broadcaster.sendMessage(NetworkCommand.EXIT);
        CloseServer();
    }
}
