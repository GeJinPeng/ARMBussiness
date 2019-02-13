using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsTool : MonoBehaviour {

    public Text tipsText;

    public float lifeTime;

    public GameObject tips;

    private void Awake()
    {
        lifeTime = 0;
        tips.SetActive(false);
        AndaUIManager.Instance.tipsTool = this;
    }

    public void OnDispawn()
    {
        tipsText.text = "";
        tips.SetActive(false);
    }

    public void SetInfo(string tisp, float duration)
    {
        lifeTime = duration;
        tips.SetActive(true);
        tipsText.gameObject.SetActive(false);
        tipsText.text = tisp;
        Invoke("InvokeSetTips", 0.25f);
    }

    public void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            if (tips.activeSelf)
            {
                OnDispawn();
            }
        }
    }
    private void InvokeSetTips()
    {
        tipsText.gameObject.SetActive(true);
    }
}
