using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewMediator : Mediator {
    [Inject]
    public ItemView itemView { get; set; }
    public override void OnRegister()
    {
        
    }
    public void SetItem(string name)
    {
        itemView.SetItem(name);
    }
    public void SetHotView(Hot[] hotArray) {
        itemView.SetHotView(hotArray);
    }
    public override void OnRemove()
    {
        
    }
}
