using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {
    private static readonly MyStack<BasePanel> sPanelStack = new MyStack<BasePanel>();
    private readonly UICompositor mStackCompositor = new UICompositor(sPanelStack);

    public void SetClose(BasePanel targetPanel) {
        //todo stack pop + destroy
//                Debug.Log("stack pop + destroy" + targetPanel.gameObject);
        mStackCompositor.PopPanel();


    }

    public BasePanel ShowOne(BasePanel targetPanel) {
        //hide and show
        return mStackCompositor.PushPanel(targetPanel);
    }

    /// <summary>
    /// 现在还没不需要用到....
    /// </summary>
    /// <param name="targetPanel"></param>
    public void ShowMore(BasePanel targetPanel) {
//        targetPanel.Show();
    }



}
