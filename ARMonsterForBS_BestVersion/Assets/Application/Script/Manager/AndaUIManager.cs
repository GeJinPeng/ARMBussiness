using System.Collections;
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

    public TipsTool tipsTool;

    public ChoseTip choseTip;

    public void OpenWaitBoard(bool state)
    {
        uIController.OpenWatiBoard(state);
    }

    public void PlayTips(string tipsCountent)
    {
        tipsTool.SetInfo(tipsCountent, 4f);
        Debug.Log("Tips:" + tipsCountent);
    }

    public void PlayCheckNetErrorTips()
    {
        tipsTool.SetInfo("请检查网络", 4f);
    }

    public void PlayTipsForBuySuccess()
    {
        tipsTool.SetInfo("购买成功", 2f);
    }

    public void PlayTipsForChoose(string content, int tipsType,string btn1Title, string btn2Title, System.Action comfrim ,System.Action cancel)
    {
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ChooseTipsView);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        view.GetComponent<ChooseTips>().SetTips(content,tipsType,btn1Title,btn2Title,comfrim,cancel);
    }

    public void PlayChoseTips(string tipsCountent , System.Action callback)
    {
        choseTip.SetInfo(tipsCountent, callback);
    }

    public void OpenPhotopickBar(Button btn,System.Action<Texture2D> callback)
    {
        uIController.OpenImageBar(btn, callback);
    }

    public void SetIntoCanvas(Transform t)
    {
        t.parent = uIController.transform;
        t.localScale = Vector3.one;
        t.localPosition = Vector3.zero;


    }


}
