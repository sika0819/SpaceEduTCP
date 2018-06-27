using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ClientTest : MonoBehaviour {
    public IPEndPoint serverPoint;
    private AsyncTcpClient asyncTcpClient;
    private int connectTime = 1;
    private int exitTime = 20;
    public bool heartBeat = false;
    public bool isClientConnected = false;
    public bool isConnected = false;
    public bool isStartTcp = false;
    private Thread clientConnectThread;
    private Thread checkHeatBeat;
    public UILabel logText;
    UDPClient udpClient;
    int id;
    string userName;
    string password;
    int serverTcpPort;
    // Use this for initialization
    void Start () {

        id = SystemInfo.graphicsDeviceID;
        userName = SystemInfo.deviceName;
#if UNITY_ANDROID
        userName= SystemInfo.deviceUniqueIdentifier;
#endif
        password = SystemInfo.deviceUniqueIdentifier;
        SaveDataController.Init(Application.persistentDataPath, "ClientData.DATA");
        clientConnectThread = Loom.StartSingleThread(ClientConnect, System.Threading.ThreadPriority.Normal, true);
        Application.quitting += Application_quitting;
    }

  

    UILabel connectLabel;
    public void StartClient(UILabel connetLabel) {
        this.connectLabel = connetLabel;
        if (!isClientConnected)
        {
            udpClient = new UDPClient(2333);
            udpClient.Start();
            udpClient.DataReceived += UdpClient_DataReceived;
            connetLabel.text = "断开";
        }
        else {
            CloseClient();
            connectLabel.text = "连接";
        }
        isClientConnected = !isClientConnected;
    }
    private void UdpClient_DataReceived(object sender, AsyncSocketEventArgs e)
    {
        LogTool.Log(e.msg);
        if (e.msg == NetworkCommand.ISCONNECTED)
        {
            serverPoint = new IPEndPoint(e.state.remote.Address, serverTcpPort);
            LogTool.Log(serverPoint);
            isConnected = true;
            startTCP();
            SaveDataController.CreateUser(id, userName, password);
            SaveDataController.SetServerPort(serverPoint.Address.ToString(), serverTcpPort);
            //closeUDP();
        }
        else if (e.msg.StartsWith(NetworkCommand.CONNECT)&&!isConnected)
        {
            serverTcpPort= int.Parse(e.msg.Split(':')[1]);
            udpClient.AsynSend(NetworkCommand.LOGIN + ";id:" + id + ";userName:" + userName + ";psd:" + password);//连接中，发送用户名密码
        }
        else if (e.msg== NetworkCommand.EXIT)
        {
            CloseClient();
            LogTool.Log("被服务器踢出");
        }   
    }
    void closeUDP() {
        if (udpClient != null)
        {
            udpClient.AsynSend(NetworkCommand.EXIT);
            udpClient.DataReceived -= UdpClient_DataReceived;
            udpClient.Dispose();
            
        }

    }
    private void startTCP() {
        if (!isStartTcp)
        {
            LogTool.Log("服务器连接成功"+serverPoint);
            asyncTcpClient = new AsyncTcpClient();
            asyncTcpClient.Connet(serverPoint);
            asyncTcpClient.OnReceived += AsyncClientReceived;
            isStartTcp = true;
        }
    }
    private void Update()
    {
       
    }
    public void Send(UIInput msgLabel) {
        if (asyncTcpClient != null) {
            asyncTcpClient.AsynSend(msgLabel.value);
            msgLabel.value = "";
        }
    }
    private void AsyncClientReceived(string msg)
    {
        LogTool.Log(msg);
        switch (msg) {
            case NetworkCommand.HEART_BEAT:
                heartBeat = true;
                asyncTcpClient.AsynSend(NetworkCommand.HEART_BEAT);
                break;
            case NetworkCommand.EXIT:
                LogTool.Log("被服务器踢出");
                Loom.DispatchToMainThread(() =>
                {
                    CloseClient();
                });
                
                break;
            default:
                LogTool.Log("server>client:" + msg);
                break;
        }
    }
    int count;
    // Update is called once per frame
    void ClientConnect () {
        try
        {
            if (asyncTcpClient != null)
            {
                while (!asyncTcpClient.isConnected && asyncTcpClient.tcpClient.Poll(connectTime, System.Net.Sockets.SelectMode.SelectRead))
                {
                    Loom.DispatchToMainThread(() => asyncTcpClient.Connet());
                    Loom.WaitForSeconds(connectTime);
                    count++;
                }
                if (count > exitTime)
                {
                    count = 0;
                    Loom.DispatchToMainThread(() => closeTCP());
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    void CheckHeartBeat() {
        while (!asyncTcpClient.isConnected)
        {
            Loom.WaitForSeconds(exitTime);
            if (!heartBeat)
            {
                LogTool.Log("连接不到服务器");
            }
            heartBeat = false;
        }
    }
    public void CloseClient() {
        
        closeUDP();
        closeTCP();
        isConnected = false;
    }
    public void closeTCP() {
        if (asyncTcpClient != null)
        {
            asyncTcpClient.AsynSend(NetworkCommand.EXIT);
            asyncTcpClient.OnReceived -= AsyncClientReceived;
            asyncTcpClient.Dispose();
            clientConnectThread.Abort();
            isStartTcp = false;
        }
    }
    public void Quit() {
        Application.Quit();
    }
    private void Application_quitting()
    {

        CloseClient();
    }
}
