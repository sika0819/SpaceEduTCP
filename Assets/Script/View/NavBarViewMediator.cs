using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBarViewMediator : Mediator {
    [Inject]
    public NavBarView navBarView { get; set; }
    
}
