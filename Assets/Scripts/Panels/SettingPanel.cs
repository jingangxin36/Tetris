using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel {
    public Button muteButton;
    public Button closeButton;
    public Button definedButton;
    public Button difficulityButton;
    public Image muteImage;
    private bool mIsMute;

    void Awake() {
        Init();
    }

    private void SetClose() {

    }

    public override BasePanel Show() {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        return this;
    }

    public override void Init() {
        panelType = 1;
        stack = true;
        muteButton.onClick.AddListener(() => {
            mIsMute = !mIsMute;
            muteImage.enabled = mIsMute;
            EventManager.Instance.Fire(UIEvent.SET_MUIE, mIsMute);
        }
        );
        closeButton.onClick.AddListener(() => UIManager.Instance.SetClose(this));
        //todo 
        //        definedButton.onClick.AddListener(() => EventManager.Instance.Fire(UIEvent.SHOW_DEFINED_PANEL));
        difficulityButton.onClick.AddListener(() => EventManager.Instance.Fire(UIEvent.SHOW_DIFFICULITY_PANEL));
    }

    public override void Hide() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void Destroy() {

    }
}
