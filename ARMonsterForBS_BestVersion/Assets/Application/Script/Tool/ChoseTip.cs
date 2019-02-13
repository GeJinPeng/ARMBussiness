using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseTip : MonoBehaviour {

    public Text tipsText;

    public GameObject tips;

    public System.Action _callback;

    private void Awake()
    {
        tips.SetActive(false);
        AndaUIManager.Instance.choseTip = this;
    }

    public void OnDispawn()
    {
        tipsText.text = "";
        _callback = null;
        tips.SetActive(false);
    }

    public void SetInfo(string tisp, System.Action callback)
    {
        tips.SetActive(true);
        tipsText.gameObject.SetActive(false);
        tipsText.text = tisp;
        _callback = callback;
        Invoke("InvokeSetTips", 0.25f);
    }
   
    private void InvokeSetTips()
    {
        tipsText.gameObject.SetActive(true);
    }


    public void Confirm()
    {
        if (_callback != null)
            _callback();
        OnDispawn();
    }
    public void Close()
    {
        OnDispawn(); 
    }
}
