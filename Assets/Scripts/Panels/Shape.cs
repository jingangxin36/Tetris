using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {
    private Transform mPivot;
    private Controller mControllerInstance;
    private bool mIsPause;
    public float normalStepTime;
    private float mTimer;
    
    private bool mIsSpeedUp;
    public bool isTest;
    private const int kMultiple = 20;

    // Use this for initialization
    void Awake() {
        
        mPivot = transform.Find("Pivot");
        mControllerInstance = Controller.Instance;
    }

    // Update is called once per frame
    void Update() {
        if (mIsPause) {
            return;
        }
        mTimer += Time.deltaTime;
        if (mTimer > (mIsSpeedUp? normalStepTime / kMultiple : normalStepTime)) {
            mTimer = 0;
            Fall();
        }
        //input
        InputControl();
    }

    public float Upgrade() {
        //升级
        normalStepTime /=2;
        return normalStepTime;
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
        lock (mPivot) {
            var newTransform = transform.position;
            newTransform.x -= 1;
            transform.position = newTransform;
            //如果不能转
            if (mControllerInstance.model.IsShapePositionValid(transform) == false) {
                newTransform.x += 1;
                transform.position = newTransform;
            }
            else {
                AudioManager.Instance.PlayControl();
            }
        }
    }

    public void StepRight() {
        lock (mPivot) {
            var newTransform = transform.position;
            newTransform.x += 1;
            transform.position = newTransform;
            //如果不能转
            if (mControllerInstance.model.IsShapePositionValid(transform) == false) {
                newTransform.x -= 1;
                transform.position = newTransform;
            }
            else {
                AudioManager.Instance.PlayControl();

            }
        }
    }

    public void SpeedUp() {
        if (mIsSpeedUp) {
            return;
        }
        mIsSpeedUp = true;
    }

    public void RotateShape() {
        transform.RotateAround(mPivot.position, Vector3.forward, -90);
        //如果不能转
        if (mControllerInstance.model.IsShapePositionValid(transform) == false) {
            transform.RotateAround(mPivot.position, Vector3.forward, 90);
        }
        else {
            AudioManager.Instance.PlayControl();

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
        else {
                AudioManager.Instance.PlayDrop();
        }

    }

    public void Resume() {
        mIsPause = false;
    }

    public void Pause() {
        mIsPause = true;
    }

    public void Init(Color color, float initSpeed) {

        //YOUNG 遍历
        foreach (Transform block in transform) {
            if (block.tag == "Block") {
                block.GetComponent<SpriteRenderer>().color = color;
            }
        }
        normalStepTime = initSpeed;
    }

    public void SetSpeed(float speed) {
        normalStepTime = speed;
    }
}
