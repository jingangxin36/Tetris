using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : BasePanel {

    public Button clearButton;
    public Button closeButton;

    public Text highestScoreText;
    // Use this for initialization
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
        EventManager.Instance.Fire(UIEvent.GET_SCORE_INFO, Convert.ToInt32(panelType));
        return this;
    }

    public override void Init() {
        panelType = 2;
        //        clearButton.onClick.AddListener(() => EventManager.Instance.Fire(UIEvent.CLEAR_DATA));
        clearButton.onClick.AddListener(() => EventManager.Instance.Fire(UIEvent.SHOW_ALERT));

        closeButton.onClick.AddListener(() => UIManager.Instance.SetClose(this));
    }

    public override void Hide() {
        highestScoreText.text = "";
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void Destroy() {
    }

    public override void UpdatePanelInfo(int[] info) {
        //info[0]>>highest
        if (info == null || info.Length == 0) {
            return;
        }
        highestScoreText.text = info[0].ToString();

    }

    private void SetClose() {

    }
}
