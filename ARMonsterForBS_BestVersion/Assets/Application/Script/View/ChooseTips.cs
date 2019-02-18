using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseTips : MonoBehaviour {


    public GameObject btn1;
    public GameObject btn2;

    public GameObject btn3;//only one

    public Text tipsText;

    public Text btnTitle1;
    public Text btnTitle2;

    public Text btnTitle3;//only one

    private System.Action callback_clickComfirm;
    private System.Action callback_clickCancel;

    public void SetTips(
        string tipsContent,int tipsType /* 0 为 2种 选择， 1为 只有一个种选择*/, string comfirmBtnCotent, string cancelBtnContent,System.Action comfim,System.Action cancel)
    {
        tipsText.text = tipsContent;
      
        btn3.gameObject.SetActive(tipsType != 0);

        if(tipsType == 0)
        {
            btn1.gameObject.SetActive(true);
            btn2.gameObject.SetActive(true);

            btnTitle1.text = comfirmBtnCotent;
            btnTitle2.text = cancelBtnContent;

            callback_clickComfirm = comfim;
            callback_clickCancel = cancel;
        }
        else
        {
            btn3.gameObject.SetActive(true);
            btnTitle3.text = comfirmBtnCotent;
            callback_clickComfirm = comfim;
        }

    }


    public void ClickComfirm()
    {
        if(callback_clickComfirm!=null)
        {
            callback_clickComfirm();
        }

        Destroy(gameObject);
    }

    public void ClickCancel()
    {
        if (callback_clickCancel != null)
        {
            callback_clickCancel();
        }
        Destroy(gameObject);

    }

}
