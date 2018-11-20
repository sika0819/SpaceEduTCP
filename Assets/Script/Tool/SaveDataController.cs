using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System;
using FlatBuffers;

public static class SaveDataController {
	static string savePath;
    public static DataBase dataBase=new DataBase();
    static FlatBufferBuilder flatBufferBuilder;
    private static int userCount;
    public static bool isFirstTime = true;
    /// <summary>
    /// 第一次打开时，将文件拷贝至本机目录下
    /// 之后读取本机目录即可
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    public static void Init(string path, string fileName)
    {
        
        savePath = path+ "/" + fileName;
        flatBufferBuilder = new FlatBufferBuilder(1024);
        LogTool.Log(savePath);
        if (File.Exists(savePath))
        {
            Load();
            if (dataBase.ByteBuffer == null)
            {
                isFirstTime = true;
            }
            else {
                isFirstTime = false;
            }
        }
        
        
    }
    public static void SetServerPort(string ipAddress,int port)
    {
        if (isFirstTime)
        {
            StringOffset ip = flatBufferBuilder.CreateString(ipAddress);
            DataBase.StartDataBase(flatBufferBuilder);
            DataBase.AddIpAddress(flatBufferBuilder, ip);
            DataBase.AddPort(flatBufferBuilder, port);
            Save();
        }
    }
    public static void CreateUser(int id,string userName, string password)
    {
        if (isFirstTime)
        {
            var loginName = flatBufferBuilder.CreateString(userName);
            var loginPsd = flatBufferBuilder.CreateString(password);
            User.StartUser(flatBufferBuilder);
            User.AddUserid(flatBufferBuilder, id);
            User.AddName(flatBufferBuilder, loginName);
            User.AddPassword(flatBufferBuilder, loginPsd);
            Offset<User>[] user = { User.EndUser(flatBufferBuilder) };
            VectorOffset UserList = DataBase.CreateUserListVector(flatBufferBuilder, user);
            DataBase.StartDataBase(flatBufferBuilder);
            DataBase.AddUserList(flatBufferBuilder, UserList);
            Save();
        }
    }
    public static void Load()
    {
        LogTool.Log(savePath);
        //if (!File.Exists(savePath)) throw new Exception("Load failed : 'DATABASE.DATA' not exis, something went wrong");
        ByteBuffer bb = new ByteBuffer(File.ReadAllBytes(savePath));

        if (!DataBase.DataBaseBufferHasIdentifier(bb))
        {
            throw new Exception("Identifier test failed, you sure the identifier is identical to the generated schema's one?");
        }
        dataBase = DataBase.GetRootAsDataBase(bb);
    }
    public static void Save()
    {
        if (isFirstTime)
        {
            var offset = DataBase.EndDataBase(flatBufferBuilder);
            DataBase.FinishDataBaseBuffer(flatBufferBuilder, offset);
            using (var ms = new MemoryStream(flatBufferBuilder.DataBuffer.ToFullArray(), flatBufferBuilder.DataBuffer.Position, flatBufferBuilder.Offset))
            {

                File.WriteAllBytes(savePath, ms.ToArray());

                ms.Dispose();
            }
            Load();
            isFirstTime = false;
        }

    }

}
