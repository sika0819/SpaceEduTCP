using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class SphereView : View {
    public Quaternion TargetRotation=Quaternion.identity;
    public Transform uiCameraTransform;
    public Transform PanelTransform;
    public UIScrollView scrollView;
    UIRoot root;
    public ItemView itemView;
    public bool isShow = false;
    TweenPosition tweenPosition;
    protected override void Start()
    {
        base.Start();
        root = FindObjectOfType<UIRoot>();
        uiCameraTransform = root.transform.Find("Camera");
        PanelTransform = uiCameraTransform.Find("Panel");
        scrollView = PanelTransform.Find("TopicScrollView").GetComponent<UIScrollView>();
        
        tweenPosition = scrollView.GetComponent<TweenPosition>();
        if (tweenPosition == null)
        {
            tweenPosition = scrollView.gameObject.AddComponent<TweenPosition>();
        }
        tweenPosition.to = new Vector3(scrollView.transform.localPosition.x, -148, scrollView.transform.localPosition.z);
        tweenPosition.duration = 1;
    }
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 5);

    }
    public void ControllRotate(Vector3 angle) {
        TargetRotation = transform.rotation * Quaternion.Euler(angle);
    }
    public void MenuPlayTween() {
        if (!isShow)
        {
            tweenPosition.PlayForward();
        }
        else
        {
            tweenPosition.PlayReverse();
        }
        isShow = !isShow;
    }
}
