using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICompositor {
    private readonly MyStack<BasePanel> mPanelStack;
    public UICompositor(MyStack<BasePanel> panelStack) {
        mPanelStack = panelStack;
    }

    public BasePanel PushPanel(BasePanel targetPanel) {
        if (targetPanel == mPanelStack.Peek()) {
            return targetPanel;
        }
        mPanelStack.Remove(targetPanel);
        var tempPanel = mPanelStack.Peek();
        if (tempPanel != null) {
            if (tempPanel.stack) {
                tempPanel.Hide();
            }
            else {
                mPanelStack.Pop();
            }
        }
//        Debug.Log(targetPanel);
        targetPanel.Show();
        return mPanelStack.Push(targetPanel);
    }

    public void PopPanel() {
        mPanelStack.Pop()?.Hide();
//        Debug.Log(mPanelStack.Peek());
        mPanelStack.Peek()?.Show();         
    }

    private void ComposeNodes() {
        
    }

    public void ClearAllPanel() {
        mPanelStack.Peek()?.Hide();
        mPanelStack.Clear();
    }
}
