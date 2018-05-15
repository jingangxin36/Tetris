using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRoolTip : MonoBehaviour {
    private float showTime = 0.8f;
    private bool isShow;
    private float mTimer;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (!isShow) {
	        return;
	    }
        mTimer += Time.deltaTime;
        if (mTimer > showTime) {
            mTimer = 0;
            gameObject.SetActive(false);
            isShow = false;
        }
    }

    void OnEnable() {
        isShow = true;
    }
}
