using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverPanel : BasePanel {
    public Text currentScoreText;
    public Text highestScoreText;
    public Text titleText;


    // Update is called once per frame
    void Update() {

    }

    public override BasePanel Show(bool isRestart = false) {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        EventManager.Instance.Fire(UIEvent.GET_SCORE_INFO);
        return this;
    }

    public override void Hide() {

        highestScoreText.text = "";
        currentScoreText.text = "";
        titleText.text = "";

        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void UpdatePanelInfo(int[] info) {
        //update current/highest score 
        //info[0]>>highest
        //info[1]>>current
        if (info == null || info.Length == 0) {
            return;
        }
        highestScoreText.text = info[0].ToString();
        currentScoreText.text = info[1].ToString();

        //set title
        titleText.text = info[0] == info[1] ? "新纪录!" : "游戏结束";
    }
}
