using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxViewMediator : Mediator {
    [Inject]
    public SkyBoxView skyBoxView { get; set; }
    [Inject]
    public InitPanoramaSignal initPanoramaSignal { get; set; }
    [Inject]
    public ITextureListModel textureListModel { get; set; }
    public override void OnRegister()
    {
        initPanoramaSignal.AddListener(OnInitView);
    }

    private void OnInitView()
    {

        skyBoxView.SetSkyBox(textureListModel.textureList[0]);
    }

    public void SetSkyBox(Texture2D skyboxTexture)
    {
        skyBoxView.SetSkyBox(skyboxTexture);
    }
    public override void OnRemove()
    {
        initPanoramaSignal.RemoveListener(OnInitView);
    }
}
