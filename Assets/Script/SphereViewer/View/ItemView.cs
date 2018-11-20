using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

public class ItemView:View
{
    private string itemName;
    private UILabel itemNameLabel;
    public UIScrollView hotView;
    Transform hotgridTransform;
    List<GameObject> hotPoints;
    GameObject hotdesTemple;
    protected override void Awake()
    {
        hotView = transform.Find("HotScrollView").GetComponent<UIScrollView>();
        hotgridTransform = hotView.transform.Find("UIGrid");
        hotdesTemple = hotgridTransform.Find("hot").gameObject;
        hotPoints = new List<GameObject>();
    }
    public void SetItem(string name) {
        itemName = name;
        itemNameLabel.text = name;
    }
    public void SetHotView(Hot[] hotArray)
    {
        for (int i = 0; i < hotArray.Length; i++) {
           GameObject temp= Instantiate<GameObject>(hotdesTemple, hotgridTransform);
            temp.transform.SetParent(hotgridTransform);
            temp.SetActive(true);
            UILabel hotdes= temp.GetComponent<UILabel>();
            hotdes.text = hotArray[i].desc;
        }
    }
}