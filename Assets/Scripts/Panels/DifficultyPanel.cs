using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyPanel : BasePanel {
    public Button closeButton;
    public ToggleGroup toggleGroup;
    public GameObject toggleGroupGameObject;
    //    private Toggle[] mToggles;

    //todo set speed step

    void Awake() {
        Init();

    }

    // Update is called once per frame
    void Update() {

    }

    public override BasePanel Show() {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        return this;
    }

    public override void Init() {
        panelType = 5;
        closeButton.onClick.AddListener(() => UIManager.Instance.SetClose(this));
        //        mToggles = toggleGroupGameObject.GetComponentsInChildren<Toggle>();

        // todo init toggles

        //        for (int i = 0; i < mToggles.Length; i++) {
        //            var index = i;
        //            mToggles[i].onValueChanged.AddListener(
        //                delegate
        //                {
        //                    if (mToggles[index].isOn) {
        //                        SwitchDifficulty(index);
        //                    }
        //                });
        //        }
    }

    private void SwitchDifficulty(int index) {
        //todo save target difficulty

    }

    private void ConfirmButtonOnClick() {
        //todo: if gaming, need to show alert >> restart game
        //todo: else fire 
    }



    public override void Hide() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void Destroy() {
    }
}
