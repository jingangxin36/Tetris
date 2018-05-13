using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState {

	void Awake () {
        stateID = StateID.PLAY;
        AddTransition(Transition.PAUSE_BUTTON_CLICK, StateID.MENU);
    }

    public override void DoBeforeEntering() {
        TimeManager.Instance.Fire(GameEvent.ENTER_PLAY_STATE);
    }
    public override void DoBeforeLeaving() {
        TimeManager.Instance.Fire(GameEvent.LEAVE_PLAY_STATE);
    }
}
