using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    private bool mIsPause = true;//游戏是否暂停
    [HideInInspector]
    public Shape currentShape;
    [HideInInspector]
    public Shape nextShape;
    private float mInitSpeed = 0.8f;
    private float mCurrentSpeed;

    public Transform blockContainer;
    public Shape[] shapes;
    public Color[] colors;

    public int maxSpeedLevel;
    private int mCurrentLevel;

    protected override void Awake() {
        base.Awake();
        mCurrentSpeed = mInitSpeed;
    }

    void Update() {
        if (mIsPause) {
            return;
        }
        if (currentShape == null) {
            SpawnShape();
        }
    }



    public void StartGame() {
        mIsPause = false;
        if (currentShape!= null) {
            currentShape.Resume();
        }
    }

    public void SpawnShape() {
        
        if (currentShape != null) {
            return;
        }
        int shapeIndex;
        int colorIndex;
        //第一次进入游戏
        if (nextShape == null) {
             shapeIndex = Random.Range(0, shapes.Length);
             colorIndex = Random.Range(0, colors.Length);
            currentShape = Instantiate(shapes[shapeIndex]);
            currentShape.Init(colors[colorIndex], mCurrentSpeed);
        }
        else {
            currentShape = nextShape;
        }
        //重置shape位置
        currentShape.transform.parent = blockContainer;
        currentShape.transform.localPosition = Vector3.zero;
        currentShape.Resume();

        shapeIndex = Random.Range(0, shapes.Length);
        colorIndex = Random.Range(0, colors.Length);
        nextShape = Instantiate(shapes[shapeIndex]);
        nextShape.Init(colors[colorIndex], mCurrentSpeed);
        nextShape.Pause();

    }

    public void ShapeFallDown() {
        if (Controller.Instance.model.IsGameOver()) {
            EventManager.Instance.Fire(UIEvent.GAME_OVER);
        }
        else currentShape = null;
    }

    public void PauseGame() {
        mIsPause = true;
        if (currentShape != null) {
            currentShape.Pause();
        }
    }

    public void RestartGame() {
        mCurrentLevel = 0;

        for (int i = 0; i < blockContainer.childCount; i++) {
            var shape = blockContainer.GetChild(i).gameObject;
            Destroy(shape);
        }
        ResetSpeed();
    }

    public void UpgradeLevel() {
        mCurrentLevel++;
//        Debug.Log("mCurrentLevel"+ mCurrentLevel);
        if (mCurrentLevel > maxSpeedLevel) {
            return;
        }
        mCurrentSpeed = currentShape.Upgrade();
        nextShape?.Upgrade();
        //todo rool tip
//        EventManager.Instance.Fire(UIEvent.UPGRADE_LEVEL);
        Controller.Instance.view.ShowUpdateRoolTip();
    }

    public void ResetSpeed() {
        currentShape?.SetSpeed(mInitSpeed);
        nextShape?.SetSpeed(mInitSpeed);
        mCurrentSpeed = mInitSpeed;
    }
}
