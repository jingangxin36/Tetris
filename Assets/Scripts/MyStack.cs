using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//只用于存放自定义类

public class MyStack<T> :  IEnumerable {

    private T[] mStackArray;
    private const int kDefaultArrayLength = 5;


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
        ArrayGC(ref mStackArray, Size);
    }

    public T Pop() {
        //todo check is empty
        if (IsEmpty()) {
            return default(T);
        }

        Size--;
        var result = mStackArray[Size];
        ArrayGC(ref mStackArray, Size);
        return result;
    }

    public T Push(T item) {
        //todo check is null
        if (Equals(item, default(T))) {
            return item;
        }

        mStackArray[Size++] = item;        
        if (Size >= mStackArray.Length - 1) {
            Resize();
        }
        return default(T);
    }

    public T Peek() {
        //todo check is empty
        if (IsEmpty()) {
            return default(T);
        }

        return mStackArray[Size-1];
    }

    public bool Remove(T item) {
        //todo check is null
        if (Equals(item, default(T))) {
            return false;
        }

        var isFound  = false;
        for (int i = 0; i < Size; i++) {
            if (!isFound && Equals(item, mStackArray[i])) {
                isFound = true;
            }
            if (isFound) {
                //if end
                if (i + 1 == Size) {
                    Size--;
                    ArrayGC(ref mStackArray, Size);
                    return true;
                }
                //if not end, copy
                mStackArray[i] = mStackArray[i + 1];
            }
        }
        return isFound;
    }

    private void Resize() {
        Debug.Log("Begine Resize: " + Peek());

        var newArray = new T[mStackArray.Length * 2];
        for (int i = 0; i < mStackArray.Length; i++) {
            newArray[i] = mStackArray[i];
        }
        mStackArray = newArray;
        Debug.Log("Finish Resize: " + mStackArray.Length);

    }
    /// <summary>
    /// 返回栈中所有元素, 一个一个列出来
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator() {
        for (int i = Size-1; i >=0; i--) {
            yield return mStackArray[i];
        }
    }

    private void ArrayGC(ref T[] array, int index) {
        for (int i = index; i < array.Length; i++) {
            if (Equals(default(T), array[i])) {
                return;
            }
            array[i] = default(T);
        }
    }
}
