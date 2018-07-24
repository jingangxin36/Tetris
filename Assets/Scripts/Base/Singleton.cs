using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    protected static T instance;

    public static T Instance => instance;

    protected virtual void Awake() {
        if (Instance == null) {
            instance = GetComponent<T>();
        }
        else {
            Debug.LogError("Something went wrong.  There should never be more than one instance of " + typeof(T));
        }
    }

}

