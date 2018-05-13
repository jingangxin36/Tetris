using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour {
    public Button startButton;
    public Button restartButton;
    public Button settingButton;
    public Button rankButton;


	void Awake () {
        startButton.onClick.AddListener(()=> TimeManager.Instance.Fire(GameEvent.ON_START_BUTTON_CLICK));
        restartButton.onClick.AddListener(() => TimeManager.Instance.Fire(GameEvent.ON_RESTART_BUTTON_CLICK));
        settingButton.onClick.AddListener(() => TimeManager.Instance.Fire(GameEvent.ON_SETTING_BUTTON_CLICK));
        rankButton.onClick.AddListener(() => TimeManager.Instance.Fire(GameEvent.ON_RANK_BUTTON_CLICK));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
