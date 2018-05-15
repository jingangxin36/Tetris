using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : BasePanel {

    public Button clearButton;
    public Text highestScoreText;
    // Use this for initialization
    void Awake() {
        clearButton.onClick.AddListener(() => EventManager.Instance.Fire(UIEvent.CLEAR_DATA));
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override BasePanel Show(bool isRestart = false) {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        return this;
    }

    public override void Hide() {
        highestScoreText.text = "";
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void UpdatePanelInfo(int[] info) {
        //info[0]>>highest
        if (info == null || info.Length == 0) {
            return;
        }
        highestScoreText.text = info[0].ToString();

    }
}
