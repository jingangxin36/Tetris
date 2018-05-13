using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private bool mIsPause = true;//游戏是否暂停

    public Shape currentShape;
    private Shape mNextShape;

    //    private Ctrl ctrl;

    public Transform blockContainer;

    public Shape[] shapes;

    public Color[] colors;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
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
        if (mNextShape == null) {
             shapeIndex = Random.Range(0, shapes.Length);
             colorIndex = Random.Range(0, colors.Length);
            currentShape = Instantiate(shapes[shapeIndex]);
            currentShape.Init(colors[colorIndex]);
        }
        else {
            currentShape = mNextShape;
        }
        //重置shape位置
        currentShape.transform.parent = blockContainer;
        currentShape.transform.localPosition = Vector3.zero;
        currentShape.Resume();

        shapeIndex = Random.Range(0, shapes.Length);
        colorIndex = Random.Range(0, colors.Length);
        mNextShape = Instantiate(shapes[shapeIndex]);
        mNextShape.Init(colors[colorIndex]);
        mNextShape.Pause();

    }

    public void ShapeFallDown() {
        if (Controller.Instance == null) {
            Debug.LogError("Controller.Instance null!!!");
            return;
        }
        if (Controller.Instance.model.IsGameOver()) {
            //todo fire game over
        }
        else currentShape = null;
    }

    public void PauseGame() {
        mIsPause = true;
        if (currentShape != null) {
            currentShape.Pause();
        }
    }
}
