using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramaViewerRoot : ContextView
{
    private void Awake()
    {
        this.context = new PanoramaViewer(this);

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
