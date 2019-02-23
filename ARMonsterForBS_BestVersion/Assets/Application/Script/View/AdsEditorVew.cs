using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class AdsEditorVew : ViewBasic {

    public ToggleGroup toggleGroup;
    public AdsEditorView_EdiorBar adsEditorView_EdiorBar;
    public ItemInfo_AdsItem itemInfo_AdsItem;

    public Text title;
    public Text leftTitle;
    public Text rightTitle;
    public GameObject leftBtn;

    public GameObject rightBtn;

    private List<AdsStruct> adsStruct = null;
    private BussinessSHRootConfig bussinessSHRootConfig;
    private BusinessStrongholdAttribute bsa ;
    private int currentIndex;
    private float lateTimer;
    public System.Action callback_updateAds;
    private List<string> titles = new List<string>
    {
        "1号广告位" ,"2号广告位" ,"3号广告位" ,"4号广告位"
    };

    public void OnDisable()
    {
        itemInfo_AdsItem.callback_clickItem = null;

        adsEditorView_EdiorBar.callbcak_saveFinsh = null;
    }

    public void OnEnable()
    {
        isMe = true;

        itemInfo_AdsItem.callback_clickItem = CallBackClickEditorBar;

        adsEditorView_EdiorBar.callbcak_saveFinsh = CallbackSaveEditor;
    }

    public void SetInfo(BusinessStrongholdAttribute _bsa, List<AdsStruct> adsStructs , BussinessSHRootConfig _bussinessSHRootConfig)
    {
        bsa =_bsa;
        adsStruct = adsStructs;
        bussinessSHRootConfig = _bussinessSHRootConfig;
        SelectAdsIndex();
    }



    public void SelectAdsIndex()
    {
        itemInfo_AdsItem.gameObject.SetActive(false);
        SetSwitchBtnState();
        SetTitle();
        if (lateTimer.Equals(0))
        {
            StartCoroutine(LateExcute());
        }else
        {
            lateTimer = 0;
        }
    }

    private IEnumerator LateExcute()
    {
        while(lateTimer < 0.25f)
        {
            lateTimer += Time.deltaTime;
            yield return null;
        }
        lateTimer = 0;
        BuildAdsItem();
    }

    private void SetSwitchBtnState()
    {
        rightBtn.gameObject.SetActive(currentIndex < bussinessSHRootConfig.adsCount - 1);
        leftBtn.gameObject.SetActive(currentIndex > 0);
    }
    private void SetTitle()
    {
        title.text = titles[currentIndex];
        if(currentIndex < bussinessSHRootConfig.adsCount-1) rightTitle.text = titles[currentIndex + 1];
        if(currentIndex>0) leftTitle.text = titles[currentIndex - 1];


    }

    public void ClickLeft()
    {
        currentIndex--;
        currentIndex = Mathf.Clamp(currentIndex, 0, bussinessSHRootConfig.adsCount);
        SelectAdsIndex();
    }

    public void ClickRight()
    {
        currentIndex++;
        currentIndex = Mathf.Clamp(currentIndex, 0, bussinessSHRootConfig.adsCount);
        SelectAdsIndex();

    }


    private void BuildAdsItem()
    {
        itemInfo_AdsItem.gameObject.SetActive(true);
        itemInfo_AdsItem.GetComponent<Animator>().Play("FadeIn");
        itemInfo_AdsItem.SetInfo(adsStruct[currentIndex]);
    }

    /// <summary>
    /// 点开编辑
    /// </summary>
    /// <param name="info">Info.</param>
    private void CallBackClickEditorBar(AdsStruct info)
    {
        isMe  = false;
        adsEditorView_EdiorBar.OpenEditorBar(bsa.strongholdIndex, info,bussinessSHRootConfig);
    }
 

    /// <summary>
    /// 编辑完成
    /// </summary>
    /// <param name="info">Info.</param>
    private void CallbackSaveEditor(AdsStruct info)
    {
        isMe = true;
        if (info == null)
        {
            return;
        }
        adsStruct[currentIndex] = info;

        //重新构建item

        BuildAdsItem();

        CallBackUpdateAds();
    }

    public void  CallBackUpdateAds()
    {
        if(callback_updateAds!=null)
        {
            callback_updateAds();
        }
    }

    private void CallBackSaveEditorForSprite(Sprite _sp, AdsStruct info)
    {
        adsStruct[currentIndex] = info;

        itemInfo_AdsItem.SetItemTexture(adsStruct[currentIndex],_sp);
    }

    private Vector3 startMousePose;
    public void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            if (isMe)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startMousePose = Input.mousePosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    //float delta = Mathf.Abs(i)
                    if (Input.mousePosition.x - startMousePose.x > 500)
                    {
                        lastView.IsYou(true);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
