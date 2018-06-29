using strange.extensions.context.api;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerRoot : ContextView {
    private void Awake()
    {
        this.context = new UIManagerContext(this);
        
    }
}
