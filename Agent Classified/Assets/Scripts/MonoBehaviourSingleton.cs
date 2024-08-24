using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly Lazy<T> LazyInstance = new Lazy<T>(FindInstanceForStart);

    public static T Instance => LazyInstance.Value;

    private static T FindInstanceForStart() => FindObjectOfType<T>();
}