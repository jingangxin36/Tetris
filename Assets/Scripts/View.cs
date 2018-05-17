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
        Test();
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
        Debug.Log(stack.Pop());
        Debug.Log(stack.Peek());//3

        foreach (var temp in stack) {
            Debug.Log(temp);//1
        }

        for (int i = 0; i < 10; i++) {
            stack.Push(i);
        }
        foreach (var temp in stack) {
            Debug.Log(temp);//1
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void TabbarOnTab(int index) {
        //game over and isRestart
        mCurrentPanel?.Hide();
        if (index == 0 || index == -1) {
            GameManager.Instance.ResetSpeed();
            //restart
            if (index == -1) {
                mCurrentPanel = panels[0].Show(true);
            }
            //start or continue
            else {

                if (mIsGameOver) {
                    mCurrentPanel = panels[0].Show(true);
                    mIsGameOver = false;
                }
                else {
                    mCurrentPanel = panels[0].Show();
                }
            }
            HideMenuTabbar();
        }
        else {
            mCurrentPanel = panels[index].Show();
        }
        EventManager.Instance.Fire(UIEvent.GET_SCORE_INFO);
        AudioManager.Instance.PlayCursor();
    }

    public void UpdatePanelInfo(int[] info) {
        mCurrentPanel?.UpdatePanelInfo(info);
    }

    public void PauseGame() {

        ShowMenuTabbar();
        panels[0].Hide();
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
}
