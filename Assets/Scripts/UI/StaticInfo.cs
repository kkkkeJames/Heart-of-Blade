using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// class for storing all CONST fields such as static dictionaries, animation curves, etc
///
/// Also provides useful access functions to them
/// </summary>


public class StaticInfoManager : MonoBehaviour
{
    public static StaticInfoManager Instance { get; private set; }

    [SerializeField] public AnimationCurve FADEIN; // UI fade animation curves
    [SerializeField] public AnimationCurve FADEOUT; // UI fade animation curves
    
    [SerializeField] public AnimationCurve TEXT_ALPHA_CYCLE_CURVE; // button on hover alpha cycle
    
    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}