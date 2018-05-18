using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePanel : MonoBehaviour {

    [HideInInspector]
    public int panelType;

    /// <summary>
    /// if need put the panel in stack, set it true
    /// </summary>
    [HideInInspector]
    public bool stack = false;

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="isRestart"></param>
    /// <returns></returns>
    public abstract BasePanel Show();
    public virtual void UpdatePanelInfo(int[] info) {}
    /// <summary>
    /// 数据初始化, 按钮监听
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// 隐藏 + (栈pop) 
    /// </summary>
    public abstract void Hide();

    public abstract void Destroy();
}
