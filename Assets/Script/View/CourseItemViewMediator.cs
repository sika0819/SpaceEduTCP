using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseItemViewMediator : Mediator {
    [Inject]
    private CourseItemView courseItemView { get; set; }

    public override void OnRegister()
    {
        
    }

    public override void OnRemove()
    {

    }
}
