using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartView : MonoBehaviour {
   

    public void OnEnable()
    {
        AndaUIManager.Instance.uIController.OpenBackground(true);
    }
    public void OnDisable()
    {
        AndaUIManager.Instance.uIController.OpenBackground(false);
    }

    public void ClickScannerBtn()
    {
        AndaUIManager.Instance.uIController.OpenScannerView();
    }

    public void ClickMapBtn()
    {

        GetComponent<Animator>().Play("FadeOut");

        Invoke("EnterMapCtr",0.8f);
    }


    private void EnterMapCtr()
    {
        AndaDataManager.Instance.mainContoller.SwitchCtrl("mapCtrl");
        Destroy(gameObject);
       
    }
}
