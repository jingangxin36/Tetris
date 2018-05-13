using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour {
    public GameObject menuView;
    public GameObject playView;
    public GameObject settingView;
    public GameObject rankView;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMenu() {
        menuView.SetActive(true);
    }

    public void HideMenu() {
        menuView.SetActive(false);
    }

    public void ShowRank() {
        rankView.SetActive(true);
    }

    public void ShowPlayView() {
        playView.SetActive(true);
    }
    public void HidePlayView() {
        playView.SetActive(false);
    }
}
