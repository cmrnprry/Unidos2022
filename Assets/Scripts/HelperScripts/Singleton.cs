using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject tmpGO = new GameObject();
                    _instance = tmpGO.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        //I think this should work? 
        if (Instance != this as T)
        {
            Destroy(gameObject);
        }
        _instance = this as T;

    }
}