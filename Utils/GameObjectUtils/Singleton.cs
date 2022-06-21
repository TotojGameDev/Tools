﻿using UnityEngine;

namespace TotojGameDev
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        /**
           Returns the instance of this singleton.
        */
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                _instance = (T) FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) +
                                   " is needed in the scene, but there is none.");
                }

                return _instance;
            }
        }
    }
}