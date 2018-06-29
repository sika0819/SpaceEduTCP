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
    
    }
    public void SetPanelActive(string panelData)
    {
        loginView.SetActive(false);
        selectCharView.SetActive(false);
        StudentPanel.SetActive(false);
        TeacherPanel.SetActive(false);
        switch (panelData)
        {
            case GlobalName.LoginPanel:
                loginView.SetActive(true);
                break;
            case GlobalName.SelectCharPanel:
                selectCharView.SetActive(true);
                break;
            case GlobalName.StudentPanel:
                StudentPanel.SetActive(true);
                break;
            case GlobalName.TeacherPanel:
                TeacherPanel.SetActive(true);
                break;
        }
    }
}
