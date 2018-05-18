
using System;
using System.Collections;
using System.Collections.Generic;
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



    void Awake() {
        Init();
    }

    public override BasePanel Show() {
        //        Debug.Log("Show");
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
        highestScoreText.text = info[0].ToString();
        currentScoreText.text = info[1].ToString();
        rowText.text = info[2].ToString();
        levelText.text = info[3].ToString();

    }
}