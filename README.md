

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

![1532258890035](C:\Users\ADMINI~1\AppData\Local\Temp\1532258890035.png)

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



---

*待补充*

### UI面板切换的分类管理

### 数字滚动动画

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