using System;
using UnityEngine.UI;


public class GameOverPanel : BasePanel {
    public Text currentScoreText;
    public Text highestScoreText;
    public Text titleText;
    public Button closeButton;


    void Awake() {
        Init();
    }

    public override BasePanel Show() {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        EventManager.Instance.Fire(UIEvent.GET_SCORE_INFO, Convert.ToInt32(panelType));
        return this;
    }

    public override void Init() {
        panelType = 3;
        closeButton.onClick.AddListener(() => UIManager.Instance.SetClose(this));

    }

    public override void Hide() {

        highestScoreText.text = "";
        currentScoreText.text = "";
        titleText.text = "";
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void Destroy() {
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
        titleText.text = info[0] == info[1] ? "!新纪录!" : "游戏结束";
    }

    private void SetClose() {

    }
}
