using System.Collections.Generic;
using System;
using UnityEngine;

public class BaseTimer{
    public int TimerID { get; set; }
    public Action OnTimer { get; set; }
}

public enum GameEvent
{
    ENTER_MENU_STATE,
    LEAVE_MENU_STATE,
    ENTER_PLAY_STATE,
    LEAVE_PLAY_STATE,
    ON_START_BUTTON_CLICK,
    ON_RANK_BUTTON_CLICK,
    ON_DESTROY_BUTTON_CLICK,
    ON_RESTART_BUTTON_CLICK,
    ON_SETTING_BUTTON_CLICK,
    ON_LEFT_BUTTON_CLICK,
    ON_RIGHT_BUTTON_CLICK,
    ON_UP_BUTTON_CLICK,
    ON_DOWN_BUTTON_CLICK,
    ON_PAUSE_BUTTON_CLICK

}

public class TimeManager : Singleton<TimeManager> {

	private readonly Dictionary<GameEvent, List<BaseTimer>> mTimerDictionary = new Dictionary<GameEvent, List<BaseTimer>>();
	private int mCurrentTimerID = 0;
	private bool mIsTimerEmpty = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public int Listen(GameEvent gameEvent, Action function) {
	    mCurrentTimerID++;
	    var newBaseTimer = new BaseTimer {//YOUNG 
	        TimerID = mCurrentTimerID,
	        OnTimer = function
	    };
	    if (!mTimerDictionary.ContainsKey(gameEvent)) {
	        var newBaseTimerList = new List<BaseTimer>();
	        try {
                mTimerDictionary.Add(gameEvent, newBaseTimerList);
            }
	        catch (Exception e) {
	            Debug.LogError(e.Message);
	        }
        }
        mTimerDictionary[gameEvent].Add(newBaseTimer);
        return mCurrentTimerID;
	}

    public void Fire(GameEvent gameEvent) {
        if (!mTimerDictionary.ContainsKey(gameEvent)) {
            Debug.LogError(gameEvent + "Not exit!");
            return;
        }
        try {
            foreach (var tempTimer in mTimerDictionary[gameEvent]) {
                tempTimer.OnTimer();
            }
        }
        catch (Exception e) {
            Debug.LogError(e.Message);
        }
    }
}
