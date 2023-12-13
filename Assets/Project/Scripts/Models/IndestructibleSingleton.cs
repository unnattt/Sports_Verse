﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Yudiz
{
    public class IndestructibleSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}