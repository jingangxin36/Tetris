using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour {
    public GameObject menuTabbar;
    public GameObject restartButton;
    public BasePanel[] panels;
    public GameObject updateRoolTip;
    private BasePanel mCurrentPanel;

    private bool mIsGameOver;

    // Use this for initialization
    void Awake() {

    }

    private void Test() {
        var stack = new MyStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        foreach (var temp in stack) {
            Debug.Log(temp); //1,2,3
        }
        Debug.Log(stack.Peek());//3
        stack.Remove(2);
        foreach (var temp in stack) {
            Debug.Log("here");
            Debug.Log(temp.ToString());//1,3
        }
        Debug.Log(stack.IsEmpty());

        Debug.Log(stack.Size);
        Debug.Log(stack.Peek());//3

        foreach (var temp in stack) {
            Debug.Log(temp);//1
        }

        for (int i = 1; i < 20; i++) {
            stack.Push(i);
        }
        foreach (var temp in stack) {
            Debug.Log(temp);//1
        }
        stack.Clear();
        Debug.Log(stack.Size);
        Debug.Log(stack.IsEmpty());

    }

    // Update is called once per frame
    void Update() {

    }

    public void TabbarOnTab(int index) {
        //game over and isRestart
//        mCurrentPanel?.Hide();//TODO
        HidePanel(mCurrentPanel);
        if (index == 0 || index == -1) {
            bool isRestart;
            //restart
            if (index == 0) {
                isRestart = true;
                GameManager.Instance.ResetSpeed();

            }
            //start or continue
            else {

                if (mIsGameOver) {
                    isRestart = true;

                    mIsGameOver = false;
                }
                else {
                    isRestart = false;

                }
            }
            mCurrentPanel = UIManager.Instance.ShowOne(panels[0]);
            HideMenuTabbar();
            EventManager.Instance.Fire(UIEvent.ENTER_PLAY_STATE, isRestart);
        }
        else {
            mCurrentPanel = UIManager.Instance.ShowOne(panels[index]);
        }
        //todo 应该不用fire新数据吧
//        EventManager.Instance.Fire(UIEvent.GET_SCORE_INFO, index);
        AudioManager.Instance.PlayCursor();
    }

    public void UpdatePanelInfo(int panelType, int[] info) {
        mCurrentPanel = panels[panelType];
        mCurrentPanel?.UpdatePanelInfo(info);
    }

    public void PauseGame() {

        ShowMenuTabbar();
//        panels[0].Hide();//TODO
        HidePanel(panels[0]);

        AudioManager.Instance.PlayCursor();

    }

    public void ShowMenuTabbar() {
        restartButton.SetActive(!mIsGameOver);
        menuTabbar.SetActive(true);
    }

    public void HideMenuTabbar() {
        menuTabbar.SetActive(false);
    }





    public void GameOver() {
        //hide start button
        mIsGameOver = true;
        ShowMenuTabbar();
        //hide play panel

        //show over panel
        TabbarOnTab(3);

    }

    public void ShowUpdateRoolTip() {
        if (!updateRoolTip.activeSelf) {
            updateRoolTip.SetActive(true);
        }
    }

    public void ShowAlert() {
        UIManager.Instance.ShowOne(panels[4]);
    }

    private void HidePanel(BasePanel targetPanel) {
        if (targetPanel != null) {
            UIManager.Instance.SetClose(targetPanel);
        }
    }

    public void ShowDifficultyPanel() {
        UIManager.Instance.ShowOne(panels[6]);

    }

    public void ShowDefinedButtonPanel() {
        UIManager.Instance.ShowOne(panels[5]);

    }
}
