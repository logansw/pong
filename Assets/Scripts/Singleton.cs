using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// </summary>
/// <typeparam name="T">The inheriting class</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T s_Instance { get; private set; }

    protected virtual void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = FindObjectOfType<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}