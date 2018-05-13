using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : Singleton<Controller> {
    private TimeManager mTimeManager;
    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;
    //    [HideInInspector]
    //    public CameraManager cameraManager;
    //    [HideInInspector]
    //    public AudioManager audioManager;

    private FSMSystem fsm;

    protected override void Awake() {
        base.Awake();
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        //        cameraManager = GetComponent<CameraManager>();
        //        audioManager = GetComponent<AudioManager>();

        mTimeManager = TimeManager.Instance;
        //todo 可以由FSMSystem来fire
        mTimeManager.Listen(GameEvent.ENTER_MENU_STATE, EnterMenuState);
        mTimeManager.Listen(GameEvent.LEAVE_MENU_STATE, LeaveMenuState);
        mTimeManager.Listen(GameEvent.ENTER_PLAY_STATE, EnterPlayState);
        mTimeManager.Listen(GameEvent.LEAVE_PLAY_STATE, LeavePlayState);
        mTimeManager.Listen(GameEvent.ON_START_BUTTON_CLICK, OnStartButtonClick);
        mTimeManager.Listen(GameEvent.ON_RANK_BUTTON_CLICK, OnRankButtonClick);
        mTimeManager.Listen(GameEvent.ON_DESTROY_BUTTON_CLICK, OnDestroyButtonClick);
        mTimeManager.Listen(GameEvent.ON_RESTART_BUTTON_CLICK, OnRestartButtonClick);
        mTimeManager.Listen(GameEvent.ON_SETTING_BUTTON_CLICK, OnSettingButtonClick);
        mTimeManager.Listen(GameEvent.ON_LEFT_BUTTON_CLICK, OnLeftButtonClick);
        mTimeManager.Listen(GameEvent.ON_RIGHT_BUTTON_CLICK, OnRightButtonClick);
        mTimeManager.Listen(GameEvent.ON_UP_BUTTON_CLICK, OnUpButtonClick);
        mTimeManager.Listen(GameEvent.ON_DOWN_BUTTON_CLICK, OnDownButtonClick);
        mTimeManager.Listen(GameEvent.ON_PAUSE_BUTTON_CLICK, OnPauseButtonClick);

    }

    private void OnLeftButtonClick() {
        GameManager.Instance.currentShape.StepLeft();
    }

    private void OnRightButtonClick() {
        GameManager.Instance.currentShape.StepRight();

    }

    private void OnUpButtonClick() {
        GameManager.Instance.currentShape.RotateShape();

    }

    private void OnDownButtonClick() {
        GameManager.Instance.currentShape.SpeedUp();

    }


    private void EnterPlayState() {
        /*
        ctrl.view.ShowGameUI(ctrl.model.Score,ctrl.model.HighScore);
        ctrl.cameraManager.ZoomIn();
        ctrl.gameManager.StartGame();
         */
        view.ShowPlayView();
        GameManager.Instance.StartGame();
    }

    private void LeavePlayState() {
        /*
        ctrl.view.HideGameUI();
        ctrl.view.ShowRestartButton();
        ctrl.gameManager.PauseGame();
         */
        view.HidePlayView();
        GameManager.Instance.PauseGame();
    }

    private void OnSettingButtonClick() {
        //todo
    }

    private void LeaveMenuState() {
        view.HideMenu();
    }

    private void EnterMenuState() {
        view.ShowMenu();
        //cameraManager.ZoomOut();
    }

    private void OnStartButtonClick() {
        /*
        ctrl.model.Start();
        ctrl.gameManager.Start();
         * 
         */
        Debug.Log("OnStartButtonClick");
        fsm.PerformTransition(Transition.START_BUTTON_CLICK);
    }

    private void OnPauseButtonClick() {
        fsm.PerformTransition(Transition.PAUSE_BUTTON_CLICK);
    }
    private void OnRankButtonClick() {
        view.ShowRank();
    }
    private void OnDestroyButtonClick() {
        //model.ClearData();
        OnRankButtonClick();
    }
    private void OnRestartButtonClick() {
        /*
         *         
        ctrl.view.Restart();>>HideGameOverUI();+UpdateGameUI(0, ctrl.model.HighScore);
        ctrl.model.Restart();
        ctrl.gameManager.Restart();
         */

        fsm.PerformTransition(Transition.START_BUTTON_CLICK);
    }

    private void Start() {
        MakeFSM();
    }

    // Update is called once per frame
    void Update() {

    }

    void MakeFSM() {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach (FSMState state in states) {
            fsm.AddState(state);
        }
        MenuState s = GetComponentInChildren<MenuState>();
        fsm.InitFSM(s);
    }
}
