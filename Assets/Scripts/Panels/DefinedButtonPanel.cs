using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefinedButtonPanel : BasePanel {
    public Button closeButton;

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
        panelType = 6;
        closeButton.onClick.AddListener(() => UIManager.Instance.SetClose(this));
    }

    public override void Hide() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void Destroy() {
        throw new System.NotImplementedException();
    }
}
