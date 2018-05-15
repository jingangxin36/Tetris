using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel {
    public Button muteButton;
    public Image muteImage;
    private bool isMute;
    // Use this for initialization
    void Awake() {
        
        muteButton.onClick.AddListener(() => {
                isMute = !isMute;
                muteImage.enabled = isMute;
                EventManager.Instance.Fire(UIEvent.SET_MUIE, isMute);
            }
        );

    }

    // Update is called once per frame
    void Update() {

    }

    public override BasePanel Show(bool isRestart = false) {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        return this;
    }

    public override void Hide() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }
}
