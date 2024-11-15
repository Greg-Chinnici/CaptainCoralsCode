using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    [SerializeField] private bool log = true;
    
    public void Log(string s)
    {
        if (log) Debug.Log(s);
    }
}
