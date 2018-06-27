using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView : View {

    public LoginPanelView loginView;
    GameObject loginPanelObject;
    public ControlPanelView StudentPanel;
    GameObject studentPanelObject;
    UIButton stuLoginBtn;
    public ControlPanelView TeacherPanel;
    GameObject teacherPanelObject;
    public SelectCharView selectCharView;
    GameObject selectPanelObject;
    private ActivePanelSingal activePanelSingal;
   
    // Use this for initialization
    public void init()
    {
        loginPanelObject = transform.Find(GlobalName.LoginPanel).gameObject;
        selectPanelObject = transform.Find(GlobalName.SelectCharPanel).gameObject;
        if ((loginView= loginPanelObject.GetComponent<LoginPanelView>())==null)
        loginView = loginPanelObject.AddComponent<LoginPanelView>();
        loginView.Init(loginPanelObject);
        selectCharView = selectPanelObject.AddComponent<SelectCharView>();
        selectCharView.Init(selectPanelObject);
        studentPanelObject = transform.Find(GlobalName.StudentPanel).gameObject;
        teacherPanelObject = transform.Find(GlobalName.TeacherPanel).gameObject;
        StudentPanel = studentPanelObject.AddComponent<ControlPanelView>();
        StudentPanel.Init(studentPanelObject);
        TeacherPanel = teacherPanelObject.AddComponent<ControlPanelView>();
        TeacherPanel.Init(teacherPanelObject);
        activePanelSingal = new ActivePanelSingal();
        activePanelSingal.AddListener(SetPanelActive);
    }
    public void DispachPanelActive(string panelName, bool isActive) {
        PanelData panelData = new PanelData();
        panelData.PanelName = panelName;
        panelData.isActive = isActive;
        activePanelSingal.Dispatch(panelData);
    }
    public void SetPanelActive(PanelData panelData) {
        switch (panelData.PanelName) {
            case GlobalName.LoginPanel:
                loginView.SetActive(panelData.isActive);
                break;
            case GlobalName.SelectCharPanel:
                selectCharView.SetActive(panelData.isActive);
                break;
            case GlobalName.StudentPanel:
                StudentPanel.SetActive(panelData.isActive);
                break;
            case GlobalName.TeacherPanel:
                TeacherPanel.SetActive(panelData.isActive);
                break;
        }
    }
}
