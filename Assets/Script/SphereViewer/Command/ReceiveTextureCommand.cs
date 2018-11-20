using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReceiveTextureCommand : Command {
    [Inject]
    public Texture downloadTex { get; set; }
    [Inject]
    public ITextureListModel textureList { get; set; }
    [Inject]
    public InitPanoramaSignal panoramaSignal { get; set; }
    public ReceiveTextureCommand() {

    }
    public override void Execute()
    {
        LogTool.Log(CommenValue.AssetPath + downloadTex.name);
        if (!Directory.Exists(CommenValue.AssetPath + CommenValue.ThumbPath))
            Directory.CreateDirectory(CommenValue.AssetPath + CommenValue.ThumbPath);
        if(!Directory.Exists(CommenValue.AssetPath+CommenValue.ImagePath))
            Directory.CreateDirectory(CommenValue.AssetPath + CommenValue.ImagePath);
        if (!File.Exists(CommenValue.AssetPath + downloadTex.name))
        {
            Texture2D texture2D = (Texture2D)downloadTex;
            File.WriteAllBytes(CommenValue.AssetPath + downloadTex.name, texture2D.EncodeToJPG());
        }
        textureList.Add(downloadTex);
        panoramaSignal.Dispatch();
    }
    
}
