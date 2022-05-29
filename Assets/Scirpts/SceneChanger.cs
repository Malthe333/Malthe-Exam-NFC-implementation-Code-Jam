using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class SceneChanger : MonoBehaviour
{
    
    public static SceneChanger instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //else
        {
            //Destroy(gameObject);
        }
    
    }

    public void LoadScene(string SceneName)
    {
    SceneManager.LoadScene(SceneName);
    Debug.Log("Changed");
    
    }
}
