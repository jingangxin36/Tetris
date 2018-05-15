
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class PlayPanel : BasePanel {
    public Text currentScoreText;
    public Text highestScoreText;
    public Button pauseButton;

    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;



    void Awake() {
        leftButton.onClick.AddListener(() => GameManager.Instance.currentShape.StepLeft());
        rightButton.onClick.AddListener(() => GameManager.Instance.currentShape.StepRight());
        upButton.onClick.AddListener(() => GameManager.Instance.currentShape.RotateShape());
        downButton.onClick.AddListener(() => GameManager.Instance.currentShape.SpeedUp());
        pauseButton.onClick.AddListener(() => EventManager.Instance.Fire(UIEvent.GAME_PAUSE));
    }

    public override BasePanel Show(bool isRestart = false) {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        //gameManager.start(isRestart)
        EventManager.Instance.Fire(UIEvent.ENTER_PLAY_STATE, isRestart);
        //fire begin game(isRestart)
        currentScoreText.text = "";
        highestScoreText.text = "";        
        return this;
    }

    public override void Hide() {

        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
            currentScoreText.text = "";
            highestScoreText.text = "";
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

    }
}