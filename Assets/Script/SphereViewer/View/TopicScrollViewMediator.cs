using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicScrollViewMediator : Mediator {
    [Inject]
    public TopicScrollView topicListView { get; set; }
    [Inject]
    public ITopicList topicList { get; set; }
    [Inject]
    public SetTopicListSignal setTopicListSignal { get; set; }
    public override void OnRegister()
    {
        setTopicListSignal.AddListener(OnSetTopicList);
    }

    private void OnSetTopicList()
    {
        
    }

    public override void OnRemove()
    {
        setTopicListSignal.RemoveListener(OnSetTopicList);
    }
}
