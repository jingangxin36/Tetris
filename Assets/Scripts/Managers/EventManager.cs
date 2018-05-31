using System.Collections.Generic;
using System;
using JetBrains.Annotations;
using UnityEngine;

public class BaseEvent {
    public UIEvent EventName { get; set; }
    public Action<object> ListenerAction { get; set; }
    [CanBeNull]
    public Action CallerAction { get; set; }
}

public enum UIEvent {
    ENTER_PLAY_STATE,
    GET_SCORE_INFO,
    GAME_PAUSE,
    GAME_OVER,
    CLEAR_DATA,
    SET_MUIE,
    REFRESH_SCORE,
    SHOW_ALERT,
    SHOW_DIFFICULITY_PANEL,
    SHOW_DEFINED_PANEL,
    CAMERA_SHAKE

}

public class EventManager : Singleton<EventManager> {
    private readonly Dictionary<UIEvent, List<BaseEvent>> mEventDictionary = new Dictionary<UIEvent, List<BaseEvent>>();

    public void Listen(UIEvent uiEvent, Action<object> listenerAction, Action callerAction = null) {
        var newUIEvent = new BaseEvent {
            EventName = uiEvent,
            ListenerAction = listenerAction,
            CallerAction = callerAction
        };
        if (!mEventDictionary.ContainsKey(uiEvent)) {
            var newBaseTimerList = new List<BaseEvent>();
            mEventDictionary.Add(uiEvent, newBaseTimerList);
        }
        mEventDictionary[uiEvent].Add(newUIEvent);
    }

    public void Fire(UIEvent uiEvent, object obj = null) {
        if (!mEventDictionary.ContainsKey(uiEvent)) {
            Debug.LogError(uiEvent + "事件不存在!!");
            return;
        }
        foreach (var @event in mEventDictionary[uiEvent]) {
            @event.CallerAction?.Invoke();
            @event.ListenerAction(obj);
        }
    }
}
