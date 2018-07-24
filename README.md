

# Unity实现俄罗斯方块

## 游戏截图:

![](https://github.com/jingangxin36/Tetris/blob/master/Demo/Demo.gif)

![](https://github.com/jingangxin36/Tetris/blob/master/Demo/Demo1.gif)


## APK下载地址:

[**MyTetris.apk**](https://github.com/jingangxin36/Tetris/releases/download/V1.1/MyTetris.apk)

## 使用方法:

- 安卓手机: 直接点击游戏界面按钮
- Editor: 直接点击play

## 开发环境:

Unity2018.1

## 部分实现介绍

### UI界面使用的MVC架构

![](https://github.com/jingangxin36/Tetris/blob/master/Demo/MVC.jpg)

View层负责响应用户事件和页面显示, Controller层负责响应游戏逻辑和作为View层和Model层的介质. View层通过发送消息来获取Model层的状态信息.

### 消息机制

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

### UI面板切换的分类管理

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



### 数字滚动动画

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

---

*待补充* 

### 行消除动画 

### 相机抖动 

### 方块急速下落 

### UI风格 

### 地图的实现 

## 更多

### 关于俄罗斯方块

### 关于小游戏AI

### 关于脚本编写中注意的问题

### 关于游戏编程框架


## GitHub项目地址:

[**jingangxin36/Tetris**](https://github.com/jingangxin36/Tetris)