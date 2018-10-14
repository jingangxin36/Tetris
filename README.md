

# Unity实现俄罗斯方块

## 1. 游戏截图:

![](https://github.com/jingangxin36/Tetris/blob/master/Demo/Demo.gif)

![](https://github.com/jingangxin36/Tetris/blob/master/Demo/Demo1.gif)


## 2. APK下载地址:

[**MyTetris.apk**](https://github.com/jingangxin36/Tetris/releases/download/V1.1/MyTetris.apk)

## 3. 使用方法:

- 安卓手机: 直接点击游戏界面按钮
- Editor: 直接点击play

## 4. 开发环境:

Unity2018.1

## 5. 技术关键点与拓展

### 5.1 关于MVC架构模式

#### MVC的优缺点

**优点:**

- **低耦合性，高内聚性**: 通过MVC的框架将一个系统分成表现层、业务逻辑层、数据访问层，比如只需要改变视图层而不需要重新编译模型和控制器代码，同时一个应用的业务流程或者业务规则的改变只需要改变模型层而不需要去修改视图层和控制器层的代码。

- **高重用性**: 可以通过不同的视图层访问到模型的数据，只需要在控制器层对数据格式做处理，而不需要修改模型层的代码。

- **可维护性**: 分离出业务层、视图层、数据层，使得代码更容易维护

**实际开发中MVC或类似框架可能带来的问题:**

- **代码繁冗**: 一个很简单的逻辑, 被封装了多次, 需要在多个代码文件中索引, 阅读效率极低. 
- **项目的混乱**: 不太专业的某些程序的惰性, 导致他们并不是真正理解MVC或者说这些框架的原理, 他们的目标只是, 把功能搞出来. 他们要么绕过框架, 穿插了很多调用, 要么整体copy别人的一个功能, 去掉逻辑, 留下骨架, 然后填充自己的逻辑, 也不管这个骨架是否适用. 这样大大增加了项目的混乱 

**更多:**


- 很多设计模式, 都是要学其思想, 解决方法的思路啊
- 对于设计模式和框架我们都应该有思考和提问：
  - 这个框架适用什么需求？解决了什么问题？
  - 在什么情况下我该用, 什么情况不该用？他带来了什么问题？
  - 是否适合我的项目, 我的团队？
  - 我是应该项目整体使用, 还是某些局部的需求使用呢？
-  [探讨：为什么在游戏开发中不使用MVC？](https://zhuanlan.zhihu.com/p/38280972)

#### 相关的设计模式和架构:

- [观察者模式](https://gpp.tkchu.me/observer.html)

- [浅谈《守望先锋》中的 ECS 构架](https://blog.codingnow.com/2017/06/overwatch_ecs.html)

- [【总结】游戏框架与架构设计（Unity为例）](https://www.cnblogs.com/sols/p/8455279.html)

- [事件队列](https://gpp.tkchu.me/event-queue.html)

- C#语法之[`event`关键字](http://msdn.microsoft.com/en-us/library/8627sbea.aspx)
### 5.2 UI界面使用的MVC架构

![](https://github.com/jingangxin36/Tetris/blob/master/Demo/MVC.jpg)

View层负责响应用户事件和页面显示, Controller层负责响应游戏逻辑和作为View层和Model层的介质. View层通过发送消息来获取Model层的状态信息.

### 5.3 消息机制

项目中实现了一个事件管理器`EventManager`, 负责事件的监听和派发.  Controller层负责事件的监听以及响应. 

`EventManager`中设置了一个字典来存放事件内容

```C#
private readonly Dictionary<UIEvent, List<BaseEvent>> mEventDictionary = new Dictionary<UIEvent, List<BaseEvent>>();
```

两个接口分别提供监听和激发事件

```C#
public void Listen(UIEvent uiEvent, Action<object> listenerAction, Action callerAction = null) 

public void Fire(UIEvent uiEvent, object obj = null) 
```

枚举来存放事件类型

```C#
public enum UIEvent {
    ENTER_PLAY_STATE,
    GET_SCORE_INFO,
    GAME_PAUSE,
    GAME_OVER,
    CLEAR_DATA,
    SET_MUIE,
    REFRESH_SCORE,
    SHOW_ALERT,
    SHOW_DIFFICULITY_PANEL,
    SHOW_DEFINED_PANEL,
    CAMERA_SHAKE
}
```

### 5.4 UI面板切换的分类管理

ui面板的切换有两种需求, 情况如下
![](https://github.com/jingangxin36/Tetris/blob/master/Demo/面板管理.jpg)

- 新窗口关闭时, 自动打开旧窗口
- 新窗口关闭时, 直接回到主界面

项目中使用一个自定义栈`MyStack`来存储管理面板,  `UICompositor` 提供显示隐藏接口, 字段`BasePanel.stack`来标记该面板被覆盖时, 是否需要保存在栈内, 以便新面板关闭时, 该面板被显示出来.

自定义栈中需要保证每个面板仅有一个缓存, 则在`Push(targetPanel)`前先移除之前该面板的记录`mPanelStack.Remove(targetPanel)`, 关键代码如下:

```C#
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

```

### 5.5 数字滚动动画

游戏界面右上角的分数为使用DOTween制作的数字滚动效果, 关键是使用`Sequence` , 每次数据更新时, 将滚动动画添加到现有队列中, 保证滚动效果不会异常

关键实现方法为:

创建一个`Sequence`并设置它的`SetAutoKill`属性为`false`, 防止它实现一次滚动动画之后就自动销毁.

```c#
//声明
private Sequence mScoreSequence;
//函数内初始化
mScoreSequence = DOTween.Sequence();
//函数内设置属性
mScoreSequence.SetAutoKill(false);
```

当数据更新时, 调用该界面负责处理该数据的方法, 此时用新的数据创建一个`Tweener`, 并加入动画序列中

```c#
mScoreSequence.Append(DOTween.To(delegate (float value) {
    //向下取整
    var temp = Math.Floor(value);
    //向Text组件赋值
    currentScoreText.text = temp + "";
}, mOldScore, newScore, 0.4f));
//将更新后的值记录下来, 用于下一次滚动动画
mOldScore = newScore;
```


### 5.6 行消除动画 

通过设置**深度**和**颜色透明度**来实现"闪烁"效果(见[前面GIF](https://github.com/jingangxin36/Tetris/blob/master/Demo/Demo.gif))

2D效果为:

![](https://github.com/jingangxin36/Tetris/blob/master/Demo/2D.jpg)

关键操作如下面3D截图

![](https://github.com/jingangxin36/Tetris/blob/master/Demo/3D.jpg)


### 5.7 方块加速 

方块加速有两种条件, 一是点击`↓`按钮实现当前方块的急速下落, 二是分数达到升级条件时, 以后的每一个方块的下落速度都会更快.  

当个方块的急速采用的增大步伐, 每次下落五个单位(普通速度是一个单位), 但是要注意, 当方块快接近底部时, 需要逐渐减小步伐, 才能找到最合适的位置(不然可能会覆盖掉原先的方块)

关键代码如下:

```c#
    private void Fall(int step = 1)
    {
        while (true)
        {
            var position = transform.position;
            position.y -= step;
            transform.position = position;
            if (mControllerInstance.model.IsShapePositionValid(transform) == false)//触碰到了底部方块, 停止下落
            {
                position.y += step;
                transform.position = position;
                if (step == 1)//为一, 方块不会发生重叠
                {
                    mIsPause = true;
                    //储存当前数据>>检测是否需要消除行
                    mControllerInstance.model.PlaceShape(transform);
                    //新shape或结束
                    GameManager.Instance.ShapeFallDown();
                    break;
                }
                step = step - 1;//如果不为1, 方块可能发生重叠
                continue;
            }
            AudioManager.Instance.PlayDrop();//继续下落
            break;
        }
    }
```

方块的普通加速下落使用的是缩小每一步的时间间隔, 在`Update`函数内更新, 关键代码如下

```c#
//kMultiple为加速因子
//private const int kMultiple = 20;

void Update() {
    if (mIsPause) {
        return;
    }
    mTimer += Time.deltaTime;
    if (mIsRocket) {
        Fall(5);
        if (!mHasRocket) {
            EventManager.Instance.Fire(UIEvent.CAMERA_SHAKE);
            mHasRocket = true;
        }
    }
    else {
        if (mTimer > (mIsSpeedUp ? normalStepTime / kMultiple : normalStepTime)) {
            mTimer = 0;
            Fall();
        }
    }
    //input
    InputControl();
}
```

### 5.8 相机抖动

使用的是DOTween提供的API, 注意相机Shake完需要设置回原位..不然它会跑偏, Σ(っ °Д °;)っ

```c#
	//mCameraVector3 为相机原始的位置

	private void CameraShake(object obj) {
        Camera.main.DOShakePosition(0.05f, new Vector3(0, 0.2f, 0)).SetEase(Ease.Linear).OnComplete(() => {
            Camera.main.transform.position = mCameraVector3;
        });
    }
```


### 5.9 地图的实现 

地图使用的是单独的相机, 每个地图方块之间的间隔为1, 方便进行计算和方块的旋转和下落. 地图的原始大小固定, 而我们看到的地图和方块的大小由相机来决定. 

### 5.10 UI风格 

参考的是[腾讯游戏创意大赛](http://gameinstitute.qq.com/innovativegames/intr)

## 6. 更多

### 6.1 关于俄罗斯方块

《游戏改变世界——游戏化如何让现实变得更美好》 中的对俄罗斯方块反馈性的描述: 

>俄罗斯方块让人欲罢不能, 除了“不可能赢”这一点外, 还在于它提供的**反馈力度**. 
>
>（1）视觉上, 一排又一排的方块“噗噗”地消失；
>
>（2）数量上, 屏幕上的分数不断上涨；
>
>（3）**性质上, 你感受到了持续上升的挑战性**（速度越来越快）. 

>哲学家 James P. Carse 曾经写道, 游戏分为两种：**一种是有尽头的游戏, 我们为了获胜而玩；一种是无尽头的游戏, 我们为了尽量长时间地玩下去而玩.**  我们玩俄罗斯方块的用意很简单, 就是把一个优秀的游戏不停地玩下去.  

### 6.2 关于小游戏AI

- GitHub上[hinesboy/ai_tetris](https://github.com/hinesboy/ai_tetris)实现了一个带有AI模式的俄罗斯方块, 使用的是[pierre-dellacheries算法](http://imake.ninja/el-tetris-an-improvement-on-pierre-dellacheries-algorithm) , 其中说到最佳的方块位置由以下几个因素共同决定, 但是权重不同
  - **Landing Height:** The height where the piece is put (= the height of the column + (the height of the piece / 2))
  - **Rows eliminated:** The number of rows eliminated.
  - **Row Transitions:** The total number of row transitions. A row transition occurs when an empty cell is adjacent to a filled cell on the same row and vice versa.
  - **Column Transitions:** The total number of column transitions. A column transition occurs when an empty cell is adjacent to a filled cell on the same column and vice versa.
  - **Number of Holes:** A hole is an empty cell that has at least one filled cell above it in the same column.
  - **Well Sums:** A well is a succession of empty cells such that their left cells and right cells are both filled.
- [通过俄罗斯方块浅谈游戏中的AI](https://blog.csdn.net/coollangzi/article/details/5765096)
- [俄罗斯方块可以永无止境地玩下去吗？](http://www.matrix67.com/blog/archives/2134)

### 6.3 关于脚本编写中注意的问题

- 使用`??`或 `?. `进行空值检查时, 可能会无意中绕过底层Unity引擎对象的生命周期检查,
  - [Possible unintended bypass of lifetime check of underlying Unity engine object](https://github.com/JetBrains/resharper-unity/wiki/Possible-unintended-bypass-of-lifetime-check-of-underlying-Unity-engine-object)
  - [CUSTOM == OPERATOR, SHOULD WE KEEP IT?](https://blogs.unity3d.com/cn/2014/05/16/custom-operator-should-we-keep-it/)
- 使用`CompareTag`而不是显式字符串比较`gameObject.tag == "TagName"`, 后者会产生额外的内存与性能消耗 , 因为`tag`属性返回的字符串是从Unity本机堆拷贝到C#托管堆的对象
  - [Use CompareTag instead of explicit string comparison](https://github.com/JetBrains/resharper-unity/wiki/Use-CompareTag-instead-of-explicit-string-comparison)

### 6.4  UGUI的性能优化问题

//todo

## 7. GitHub项目地址:

[**jingangxin36/Tetris**](https://github.com/jingangxin36/Tetris)
