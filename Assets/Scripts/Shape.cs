using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {
    private Transform pivot;
    private Controller mControllerInstance;
    private bool mIsPause;
    private const float kNormalStepTime = 0.8f;//0.8
    private float mTimer = 0;
    
    private const float kSpeedUpStepTime = 0.04f;
    private bool isSpeedUp;
    // Use this for initialization
    void Awake() {
        
        pivot = transform.Find("Pivot");
        //YOUNG为什么再fall里面不能直接获取?
        mControllerInstance = Controller.Instance;
    }

    // Update is called once per frame
    void Update() {
        if (mIsPause) {
            return;
        }
        mTimer += Time.deltaTime;
        if (mTimer > (isSpeedUp? kSpeedUpStepTime : kNormalStepTime)) {
            mTimer = 0;
            Fall();
        }
        //input
        InputControl();
    }

    private void InputControl() {

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            StepLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)) {
            StepRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            RotateShape();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            SpeedUp();
        }

    }

    public void StepLeft() {
        lock (pivot) {
            var newTransform = transform.position;
            newTransform.x -= 1;
            transform.position = newTransform;
            //如果不能转
            if (mControllerInstance.model.IsShapePositionValid(transform) == false) {
                newTransform.x += 1;
                transform.position = newTransform;
            }
            else {
                //todo play sound
            }
        }
    }

    public void StepRight() {
        lock (pivot) {
            var newTransform = transform.position;
            newTransform.x += 1;
            transform.position = newTransform;
            //如果不能转
            if (mControllerInstance.model.IsShapePositionValid(transform) == false) {
                newTransform.x -= 1;
                transform.position = newTransform;
            }
            else {
                //todo play sound
            }
        }
    }

    public void SpeedUp() {
        if (isSpeedUp) {
            return;
        }
        isSpeedUp = true;
    }

    public void RotateShape() {
        transform.RotateAround(pivot.position, Vector3.forward, -90);
        //如果不能转
        if (mControllerInstance.model.IsShapePositionValid(transform) == false) {
            transform.RotateAround(pivot.position, Vector3.forward, 90);
        }
        else {
            //todo play sound
        }
    }

    private void Fall() {
        var position = transform.position;
        position.y -= 1;
        transform.position = position;
        if (mControllerInstance.model.IsShapePositionValid(transform) == false) {
            position.y += 1;
            transform.position = position;
            mIsPause = true;

            //储存当前数据>>检测是否需要消除行
            mControllerInstance.model.PlaceShape(transform);

            //新shape或结束
            GameManager.Instance.ShapeFallDown();
        }

    }

    public void Resume() {
        mIsPause = false;
    }

    public void Pause() {
        mIsPause = true;
    }

    public void Init(Color color) {

        //YOUNG 遍历
        foreach (Transform block in transform) {
            if (block.tag == "Block") {
                block.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}
