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
            //如果新窗口关闭时, 它不需要再被显示
            if (!tempPanel.stack) {
                mPanelStack.Pop();
            }
            //不管有没有弹出栈, 都需要将它隐藏
            tempPanel.Hide();
        }
        targetPanel.Show();
        return mPanelStack.Push(targetPanel);
    }

    public void PopPanel() {
        mPanelStack.Pop()?.Hide();
        mPanelStack.Peek()?.Show();         
    }

    public void ClearAllPanel() {
        mPanelStack.Peek()?.Hide();
        mPanelStack.Clear();
    }

}
