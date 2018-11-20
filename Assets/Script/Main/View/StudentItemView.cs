using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentItemView:View  {
    public GameObject ItemObject;
    public bool isChecked {
        get {
            return checkBox.value;
        }
    }
    private UILabel ipLabel;
    private UILabel userNameLabel;
    private UIToggle checkBox;
    
    private UIGrid grid;
    public string IPAddress {
        set
        {
            if (ipLabel != null)
            {
                ipLabel.text = value;
            }
        }
    }
    public string UserName
    {
        set
        {
            if (userNameLabel != null)
            {
                userNameLabel.text = value;
            }
        }
        get {
            return userNameLabel.text;
        }
    }
    public StudentItem studentItem;
    public AsyncSocketState studentState;
    public void InitItemView(GameObject copy) {
        grid = copy.transform.parent.GetComponent<UIGrid>();
        grid.enabled = true;
        ItemObject = NGUITools.AddChild(grid.gameObject,copy);
        ItemObject.transform.SetParent(grid.transform);
        ItemObject.SetActive(true);
        ipLabel = ItemObject.transform.Find("ipLabel").GetComponent<UILabel>();
        userNameLabel = ItemObject.transform.Find("userNameLabel").GetComponent<UILabel>();
        checkBox = ItemObject.transform.Find("stuCheckBox").GetComponent<UIToggle>();
        
        grid.Reposition();
    }
    public void InitItem(StudentItem item) {
        studentItem = item;
        UserName = item.UserName;
        IPAddress = item.IPAddress;
        studentState = item.studentState;
    }
    public void Destory()
    {
        ItemObject.transform.SetParent(grid.transform.parent);
        NGUITools.Destroy(ItemObject);

        grid.Reposition();
    }
}
