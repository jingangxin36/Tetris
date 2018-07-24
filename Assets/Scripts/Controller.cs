using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Controller : Singleton<Controller> {
    //also used in other class
    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;

    private EventManager mEventManager;
    private Vector3 mCameraVector3;

    protected override void Awake() {
        base.Awake();
        mCameraVector3 = Camera.main.transform.position;
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        mEventManager = EventManager.Instance;
        mEventManager.Listen(UIEvent.ENTER_PLAY_STATE, EnterPlayState);
        mEventManager.Listen(UIEvent.GET_SCORE_INFO, GetScoreInfo);
        mEventManager.Listen(UIEvent.GAME_PAUSE, GamePause);
        mEventManager.Listen(UIEvent.GAME_OVER, GameOver);
        mEventManager.Listen(UIEvent.CLEAR_DATA, ClearData);
        mEventManager.Listen(UIEvent.SET_MUIE, SetMute);
        mEventManager.Listen(UIEvent.REFRESH_SCORE, GetScoreInfo);
        mEventManager.Listen(UIEvent.SHOW_ALERT, ShowAlert);
        mEventManager.Listen(UIEvent.SHOW_DEFINED_PANEL, ShowDefinedButtonPanel);
        mEventManager.Listen(UIEvent.SHOW_DIFFICULITY_PANEL, ShowDifficultyPanel);
        mEventManager.Listen(UIEvent.CAMERA_SHAKE, CameraShake);
    }

    private void CameraShake(object obj) {
        Camera.main.DOShakePosition(0.05f, new Vector3(0, 0.2f, 0)).SetEase(Ease.Linear).OnComplete(() => {
            Camera.main.transform.position = mCameraVector3;
        });
    }

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
        Debug.Assert(obj != null, nameof(obj) + " != null");
        var isMute = (bool)obj;
        AudioManager.Instance.SetMute(isMute);
    }

    private void ClearData(object obj = null) {
        Debug.Assert(obj != null, nameof(obj) + " != null");
        var panelType = (int)obj;
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
        Debug.Assert(obj != null, nameof(obj) + " != null");
        var panelType = (int)obj;
        view.UpdatePanelInfo(panelType, model.GetScoreInfo());
    }

    private void EnterPlayState(object obj = null) {
        Debug.Assert(obj != null, nameof(obj) + " != null");
        var isRestart = (bool)obj;
        if (isRestart) {
            model.RefreshGame();
        }
        GameManager.Instance.StartGame();
    }


}
