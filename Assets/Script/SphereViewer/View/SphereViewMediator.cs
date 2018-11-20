using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Behaviors;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures.Base;
using UnityEngine;

public class SphereViewMediator : Mediator {
    [Inject]
    public SphereView sphereView { get; set; }
    public int RotationSpeed = 100;
    private TransformGestureBase transformGesture;
    private LongPressGesture longPressGesture;
    public override void OnRegister()
    {
        transformGesture = GetComponent<TransformGestureBase>();
        longPressGesture = GetComponent<LongPressGesture>();
        if (transformGesture!=null)
        transformGesture.Transformed += OnTransformGesture;
        longPressGesture.StateChanged += ShowMenu;
    }

    private void ShowMenu(object sender, GestureStateChangeEventArgs e)
    {
        if (e.State == Gesture.GestureState.Recognized)
        {
            sphereView.MenuPlayTween();
        }
    }

    private void OnTransformGesture(object sender, EventArgs e)
    {
        var rotationAngle = new Vector3(transformGesture.DeltaPosition.y / Screen.height * RotationSpeed,
                     -transformGesture.DeltaPosition.x / Screen.width * RotationSpeed,
                     0);
            sphereView.ControllRotate(rotationAngle);
    }

    public override void OnRemove()
    {
        transformGesture.Transformed -= OnTransformGesture;
        longPressGesture.StateChanged -= ShowMenu;
    }
}
