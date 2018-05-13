using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayView : MonoBehaviour {
    public Button LeftButton;
    public Button RightButton;
    public Button UpButton;
    public Button DownButton;
    public Button PauseButton;


    void Awake() {
        LeftButton.onClick.AddListener(() => TimeManager.Instance.Fire(GameEvent.ON_LEFT_BUTTON_CLICK));
        RightButton.onClick.AddListener(() => TimeManager.Instance.Fire(GameEvent.ON_RIGHT_BUTTON_CLICK));
        UpButton.onClick.AddListener(() => TimeManager.Instance.Fire(GameEvent.ON_UP_BUTTON_CLICK));
        DownButton.onClick.AddListener(() => TimeManager.Instance.Fire(GameEvent.ON_DOWN_BUTTON_CLICK));
        PauseButton.onClick.AddListener(() => TimeManager.Instance.Fire(GameEvent.ON_PAUSE_BUTTON_CLICK));

    }

    // Update is called once per frame
    void Update() {

    }
}
