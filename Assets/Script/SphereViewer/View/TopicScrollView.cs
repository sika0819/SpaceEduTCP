using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

public class TopicScrollView : View {
    public UIGrid grid;
    public GameObject templeView;
    public List<ItemView> itemViewlist; 
    protected override void Start()
    {
        grid = transform.Find("UIGrid").GetComponent<UIGrid>();
        templeView = grid.transform.Find("Item").gameObject;
        templeView.SetActive(false);
        itemViewlist = new List<ItemView>();
        base.Start();
    }
    public void SetTopicView(TopicList topicList) {
        for (int i = 0; i < topicList.topics.Length; i++)
        {
            GameObject temp = Instantiate<GameObject>(templeView);
            ItemView item = temp.GetComponent<ItemView>();

        }
    }
}
