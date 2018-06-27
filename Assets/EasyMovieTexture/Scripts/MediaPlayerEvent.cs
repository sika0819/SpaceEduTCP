using UnityEngine;
using System.Collections;

public class MediaPlayerEvent : MonoBehaviour {


	public MediaPlayerCtrl m_srcVideo;

	// Use this for initialization
	void Start () {
		m_srcVideo.OnReady += OnReady;
		m_srcVideo.OnVideoFirstFrameReady += OnFirstFrameReady;
		m_srcVideo.OnVideoError += OnError;
		m_srcVideo.OnEnd += OnEnd;
		m_srcVideo.OnResize += OnResize;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnReady() {

		LogTool.Log ("OnReady");
	}

	void OnFirstFrameReady() {
		LogTool.Log ("OnFirstFrameReady");
	}

	void OnEnd() {
		LogTool.Log ("OnEnd");
	}

	void OnResize()
	{
		LogTool.Log ("OnResize");
	}

	void OnError(MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCodeExtra){
		LogTool.Log ("OnError");
	}
}
