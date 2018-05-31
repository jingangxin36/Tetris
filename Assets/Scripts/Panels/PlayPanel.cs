
using System;
using DG.Tweening;
using UnityEngine.UI;

public  class PlayPanel : BasePanel {
    public Text currentScoreText;
    public Text highestScoreText;
    public Text rowText;
    public Text levelText;
    public Button pauseButton;

    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;
    public Button rocketButton;

    private Sequence mScoreSequence;



    void Awake() {
        Init();
    }

    public override BasePanel Show() {

        panelType = 0;
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        currentScoreText.text = "0";
        highestScoreText.text = "0";
        rowText.text = "0";
        levelText.text = "0";
        EventManager.Instance.Fire(UIEvent.GET_SCORE_INFO, Convert.ToInt32(panelType));
        return this;
    }

    public override void Init() {
        panelType = 0;
        mScoreSequence = DOTween.Sequence();
        mScoreSequence.SetAutoKill(false);
        leftButton.onClick.AddListener(() => GameManager.Instance.currentShape.StepLeft());
        rightButton.onClick.AddListener(() => GameManager.Instance.currentShape.StepRight());
        upButton.onClick.AddListener(() => GameManager.Instance.currentShape.RotateShape());
        downButton.onClick.AddListener(() => GameManager.Instance.currentShape.SpeedUp());
        pauseButton.onClick.AddListener(() => EventManager.Instance.Fire(UIEvent.GAME_PAUSE));
        rocketButton.onClick.AddListener(() => GameManager.Instance.currentShape.Rocket());
    }

    public override void Hide() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void Destroy() {

    }



    private int mOldScore = 0;


    public override void UpdatePanelInfo(int[] info) {
        //update data
        //info[0]>>highest score
        //info[1]>>current score
        //info[2]>>row
        //info[3]>>level

        if (info == null || info.Length == 0) {
            return;
        }

        int newScore = info[1];
        mScoreSequence.Append(DOTween.To(delegate (float value) {
            var temp = Math.Floor(value);
            currentScoreText.text = temp + "";
        }, mOldScore, newScore, 0.4f));
        mOldScore = newScore;

        highestScoreText.text = info[0].ToString();
        currentScoreText.text = info[1].ToString();
        rowText.text = info[2].ToString();
        levelText.text = info[3].ToString();

    }
}