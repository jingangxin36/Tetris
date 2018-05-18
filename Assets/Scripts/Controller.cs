using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : Singleton<Controller> {
    private EventManager mEventManager;
    //also used in other class
    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;
    //    [HideInInspector]
    //    public CameraManager cameraManager;


    protected override void Awake() {
        base.Awake();
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        //        cameraManager = GetComponent<CameraManager>();

        mEventManager = EventManager.Instance;
        mEventManager.Listen(UIEvent.ENTER_PLAY_STATE, EnterPlayState);


//new
        mEventManager.Listen(UIEvent.GET_SCORE_INFO, GetScoreInfo);
        mEventManager.Listen(UIEvent.GAME_PAUSE, GamePause);
        mEventManager.Listen(UIEvent.GAME_OVER, GameOver);
        mEventManager.Listen(UIEvent.CLEAR_DATA, ClearData);
        mEventManager.Listen(UIEvent.SET_MUIE, SetMute);
        mEventManager.Listen(UIEvent.REFRESH_SCORE, GetScoreInfo);
        mEventManager.Listen(UIEvent.SHOW_ALERT, ShowAlert);
        mEventManager.Listen(UIEvent.SHOW_DEFINED_PANEL, ShowDefinedButtonPanel);
        mEventManager.Listen(UIEvent.SHOW_DIFFICULITY_PANEL, ShowDifficultyPanel);
//        mEventManager.Listen(UIEvent.UPGRADE_LEVEL, UpgradeLevel);
    }

//    private void UpgradeLevel(object obj) {
//        
//        view.ShowUpdateRoolTip();
//        model.UpgradeLevel();
//    }

    private void ShowDifficultyPanel(object obj) {
        view.ShowDifficultyPanel();

    }

    private void ShowDefinedButtonPanel(object obj) {
        view.ShowDefinedButtonPanel();

    }

    private void ShowAlert(object obj = null) {
        view.ShowAlert();
    }


    private void SetMute(object obj = null) {
        var isMute = (bool)obj;
        AudioManager.Instance.SetMute(isMute);
    }

    private void ClearData(object obj = null) {
        int panelType = (int)obj;
        model.ClearData();
        view.UpdatePanelInfo(panelType, model.GetScoreInfo());
    }

    private void GameOver(object obj = null) {
        
        view.GameOver();
    }

    private void GamePause(object obj = null) {
        view.PauseGame();
        GameManager.Instance.PauseGame();
    }

    private void GetScoreInfo(object obj = null) {
//        Debug.Log(obj);
        int panelType = (int)obj;
        view.UpdatePanelInfo(panelType, model.GetScoreInfo());
    }


    private void Start() {
    }



    void Update() {

    }




    private void EnterPlayState(object obj = null) {
        /*
        ctrl.view.ShowGameUI(ctrl.model.Score,ctrl.model.HighScore);
        ctrl.cameraManager.ZoomIn();
        ctrl.gameManager.StartGame();
         */
        var isRestart = (bool)obj;
        if (isRestart) {
            model.RefreshGame();
        }
        GameManager.Instance.StartGame();

        //if restart model.clear shape&data
    }


}
