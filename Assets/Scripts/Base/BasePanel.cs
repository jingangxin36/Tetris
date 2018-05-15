using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePanel : MonoBehaviour {


    public abstract BasePanel Show(bool isRestart = false);
    public abstract void Hide();
    public virtual void UpdatePanelInfo(int[] info) {}
}
