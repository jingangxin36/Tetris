using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : Singleton<Controller> {
    private EventManager mEventManager;
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
    }

 

    private void SetMute(object obj = null) {
        var isMute = (bool)obj;
        AudioManager.Instance.SetMute(isMute);
    }

    private void ClearData(object objobj = null) {
        model.ClearData();
        view.UpdatePanelInfo(model.GetScoreInfo());
    }

    private void GameOver(object obj = null) {
        
        view.GameOver();
    }

    private void GamePause(object obj = null) {
        view.PauseGame();
        GameManager.Instance.PauseGame();
    }

    private void GetScoreInfo(object obj = null) {
        view.UpdatePanelInfo(model.GetScoreInfo());
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
        GameManager.Instance.StartGame();
        var isRestart = (bool)obj;
        if (isRestart) {
            model.RefreshGame();
        }
        //if restart model.clear shape&data
    }


}
