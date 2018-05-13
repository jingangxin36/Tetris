using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState {
    private void Awake() {
        stateID = StateID.MENU;
        AddTransition(Transition.START_BUTTON_CLICK, StateID.PLAY);
    }

    public override void DoBeforeEntering() {
        TimeManager.Instance.Fire(GameEvent.ENTER_MENU_STATE);
    }
    public override void DoBeforeLeaving() {
        TimeManager.Instance.Fire(GameEvent.LEAVE_MENU_STATE);
    }
}
