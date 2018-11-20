using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMangerView : View {
    Camera uiCamera;
    UIPanel panel;
    UIProgressBar progressBar;
    GameObject progressObj;
    EventDelegate onClose;
    UIButton closeBtn;
    protected override void Start()
    {
        base.Start();
        uiCamera = transform.Find("Camera").GetComponent<Camera>();
        panel = uiCamera.transform.Find("Panel").GetComponent<UIPanel>();
        progressObj = panel.transform.Find("Progress").gameObject;
        closeBtn= panel.transform.Find("closeBtn").GetComponent<UIButton>();
        progressObj.SetActive(false);
        progressBar = progressObj.GetComponent<UIProgressBar>();
        onClose = new EventDelegate(OnCloseApp);
        closeBtn.onClick.Add(onClose);
    }

    private void OnCloseApp()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }



    public void SetProgress(float value)
    {
        if (progressBar.value == 1f)
        {
            progressObj.SetActive(false);
        }
        else
        {
            progressObj.SetActive(true);
            progressBar.value = value;
        }
    }
}
