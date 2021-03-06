﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AndaUIManager {

    private static AndaUIManager _instance = null;

    public static AndaUIManager Instance
    {
        get {
            if(_instance == null)
            {
                _instance = new AndaUIManager();
            }
            return _instance;
        }
    }

    public UIController uIController;


    public void OpenWaitBoard(bool state)
    {
        uIController.OpenWatiBoard(state);
    }

    public void PlayTips(string tipsCountent)
    {
        Debug.Log("Tips:" + tipsCountent);
    }

    public void OpenPhotopickBar(Button btn,System.Action<Texture2D> callback)
    {
        uIController.OpenImageBar(btn, callback);
    }
}
