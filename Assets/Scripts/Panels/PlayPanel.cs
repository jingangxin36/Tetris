
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
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

    private readonly Sequence mScoreSequence;



    void Awake() {
        Init();
    }

    public override BasePanel Show() {

        panelType = 0;
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        //gameManager.start(isRestart)
//        EventManager.Instance.Fire(UIEvent.ENTER_PLAY_STATE, isRestart);
        //fire begin game(isRestart)
        currentScoreText.text = "0";
        highestScoreText.text = "0";
        rowText.text = "0";
        levelText.text = "0";
        EventManager.Instance.Fire(UIEvent.GET_SCORE_INFO, Convert.ToInt32(panelType));
        return this;
    }

    public override void Init() {
        panelType = 0;
        mScoreSequence.SetAutoKill(false);
        leftButton.onClick.AddListener(() => GameManager.Instance.currentShape.StepLeft());
        rightButton.onClick.AddListener(() => GameManager.Instance.currentShape.StepRight());
        upButton.onClick.AddListener(() => GameManager.Instance.currentShape.RotateShape());
        downButton.onClick.AddListener(() => GameManager.Instance.currentShape.SpeedUp());
        pauseButton.onClick.AddListener(() => EventManager.Instance.Fire(UIEvent.GAME_PAUSE));
    }

    public override void Hide() {

        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
            currentScoreText.text = "";
            highestScoreText.text = "";
        }
    }

    public override void Destroy() {
        throw new System.NotImplementedException();
    }



    private int mOldScore = 0;
    public PlayPanel(Sequence scoreSequence) {
        mScoreSequence = scoreSequence;
    }

    public override void UpdatePanelInfo(int[] info) {


        //update data
        //info[0]>>highest score
        //info[1]>>current score
        //info[2]>>row
        //info[3]>>level

        //todo check
        if (info == null || info.Length == 0) {
            return;
        }
        //        foreach (var i in info) {
        //            Debug.Log(i);
        //        }

        int newScore = info[1];
//        mScoreSequence.Append(DOTween.To(delegate (){
//            return 0.0f;
//        }, delegate(double value) {
//            var temp = Math.Floor(value);
//            currentScoreText.text = temp + "";
//
//        }, oldScore, newScore, 0.6));
        mScoreSequence.Append(DOTween.To(delegate (float value) {
            var temp = Math.Floor(value);
            currentScoreText.text = temp + "";
        }, mOldScore, newScore, 0.4f));
        mOldScore = newScore;
//        highestScoreText.text = info[0].ToString();

        //todo set tween
        currentScoreText.text = info[1].ToString();
        //todo set tween
        rowText.text = info[2].ToString();
        //todo set tween
        levelText.text = info[3].ToString();

    }



//    private Tweener SetRollNumberText() {
//        
//    }


}