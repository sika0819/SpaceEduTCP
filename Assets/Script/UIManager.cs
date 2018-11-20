using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    UIPanel panel;
    UILabel LogLabel;
    UIPopupList popupList;
    EventDelegate eventDelegate;
	// Use this for initialization
	void Start () {
        panel = transform.Find("Camera").Find("Panel").GetComponent<UIPanel>();
        LogLabel = panel.transform.Find("LogLabel").GetComponent<UILabel>();
        popupList = panel.transform.Find("PopupList").GetComponent<UIPopupList>();
        eventDelegate = new EventDelegate(OnSelectChange);
        popupList.onChange.Add(eventDelegate);
        popupList.AddItem(NetworkCommand.DEFAULT);
        
   
       
    }

    private void OnSelectChange()
    {
        if (ServerController.Instance.isTcpStart) {
            ServerController.Instance.SendAllClient(NetworkCommand.FOLLOW + ":" + popupList.value);
        }
    }

    // Update is called once per frame
    void Update () {
        LogLabel.text = LogTool.Message;

    }
    public void Back() {
        SceneManager.LoadScene("MainScene");
    }
}
