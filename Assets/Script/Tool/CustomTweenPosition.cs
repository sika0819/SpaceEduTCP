using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTweenPosition : TweenPosition {
    public EventDelegate OnFinshMove { get; private set; }
    private UISprite spriteUI;
    public override void Awake()
    {
        UIRoot panel = Camera.main.transform.parent.GetComponent<UIRoot>();
        float width = panel.manualWidth;
        spriteUI = GetComponent<UISprite>();
        //spriteUI.width = (int)(Screen.width*0.5f);
        //spriteUI.height = Screen.height;
        from = transform.localPosition;
        to = new Vector3(transform.localPosition.x+spriteUI.width, 0, 0);
        OnFinshMove = new EventDelegate(OnFinsh);
        onFinished.Add(OnFinshMove);
        base.Awake();
    }
    void OnFinsh()
    {

    }
}
