using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyStack<T> :  IEnumerable, IEnumerator {

    private T[] mStackArray;
//    private int mStackSize;
    private const int kDefaultArrayLength = 5;

    private int position = -1;

    public MyStack() {
        mStackArray = new T[kDefaultArrayLength];
        Size = 0;
    }

    public int Size { get;  private set; } 

    public bool IsEmpty() {
        return Size == 0;
    }

    public void Clear() {
        Size = 0;
        position = -1;
        //todo 数组 = null?
        for (int i = 0; i < Size; i++) {
            mStackArray[i] = default(T);
        }
    }

    public T Pop() {
        Debug.Log("Pop"); //1,2,3
        position--;

        return mStackArray[Size--];
    }

    public void Push(T item) {
        Debug.Log("Push"); //1,2,3

        Size++;
        position = Size++;
        //todo if need resize
        if (Size == mStackArray.Length) {
            Resize();
        }
        mStackArray[Size] = item;
    }

    public T Peek() {
        Debug.Log("Peek"); //1,2,3

        return mStackArray[Size];
    }

    public bool Remove(T item) {
        Debug.Log("Remove"); //1,2,3

        var isFound  = false;
        for (int i = 0; i < Size; i++) {
            if (Equals(item, mStackArray[i])) {
                isFound = true;
            }
            if (isFound) {
                if ((i + 1) < Size &&　Equals(default(T), mStackArray[i+1])) {
                    return true ;
                }
                mStackArray[i] = mStackArray[i + 1];
            }
        }
        return isFound;
    }

    private void Resize() {
        Debug.Log("Resize"); //1,2,3

        var newArray = new T[Size * 2];
        for (int i = 0; i < mStackArray.Length; i++) {
            newArray[i] = mStackArray[i];
            mStackArray[i] = default(T);//todo if need
        }
        mStackArray = newArray;
    }
    /// <summary>
    /// 返回栈中所有元素, 一个一个列出来
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator() {
        for (int i = Size-1; i <=0; i--) {
            yield return mStackArray[i];
        }
    }

    public bool MoveNext() {
        if (position<= 0) {
            return false;
        }
        position--;
        return true;
    }

    public void Reset() {
        Clear();
    }

    public object Current {
        get { return Peek(); }
    }
}
