using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class AdsEditorVew : MonoBehaviour {

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

    private List<string> titles = new List<string>
    {
        "1号广告位" ,"2号广告位" ,"3号广告位" ,"4号广告位"
    };
    public void OnEnable()
    {
        StartView();
        SetInfo(AndaDataManager.Instance.mainData.businessStrongholdAttributes[0]);
    }

    public void StartView()
    {
        itemInfo_AdsItem.callback_clickItem = CallBackClickEditorBar;

        //adsEditorView_EdiorBar.callbcak_saveFinshForSprite = CallBackSaveEditorForSprite;
        adsEditorView_EdiorBar.callbcak_saveFinsh = CallbackSaveEditor;
    }

    public void SetInfo(BusinessStrongholdAttribute businessStrongholdAttribute)
    {
        bsa = businessStrongholdAttribute;
        bsa.strongholdLevel =3;
        /*bsa.adsInfos = new List<AdsStruct>
        {
            new AdsStruct()
            {
                itemIndex = 0,
                type = "text",
                content = "这是一条广告",
            },
            new AdsStruct
            {
                itemIndex = 1,
                type = "texture",
                content = "UserAdsPath23400003",
            },
            new AdsStruct
            {
                itemIndex = 2,
                type = "video",
                content = "http://192.168.1.158.vvs.mp4",
            }
        }; //以上为测试数据*/

        bussinessSHRootConfig = GetGameConfigData.GetBussinessSHRootConfigItem(bsa.strongholdLevel);
        ResetAdsSturct(bsa.adsInfos);
        SetAdsNewItem();
        SelectAdsIndex();
    }

    /// <summary>
    /// 复制内容
    /// </summary>
    /// <param name="infos">Infos.</param>
    private void ResetAdsSturct( List<AdsStruct> infos)
    {
        adsStruct = new List<AdsStruct>();
        if(infos!=null && infos.Count!=0)
        {
            int count = infos.Count;
            for (int i = 0; i < count; i++)
            {
                AdsStruct ad = new AdsStruct();
                ad.itemIndex = infos[i].itemIndex;
                ad.content = infos[i].content;
                ad.type = infos[i].type;
                adsStruct.Add(ad);
            }

        }

    }

    /// <summary>
    /// 补齐空缺
    /// </summary>
    private void SetAdsNewItem()
    {
        int count = bussinessSHRootConfig.adsCount; 
        for(int i = 0; i < count; i ++ )
        {
            if(i >= adsStruct.Count)
            {
                AdsStruct ad = new AdsStruct();
                ad.itemIndex = i;
                ad.content = "+";
                ad.type = "newItem";
                adsStruct.Add(ad);
            }
        }
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
        adsEditorView_EdiorBar.OpenEditorBar(info,bussinessSHRootConfig);
    }
 

    /// <summary>
    /// 编辑完成
    /// </summary>
    /// <param name="info">Info.</param>
    private void CallbackSaveEditor(AdsStruct info)
    {
        adsStruct[currentIndex] = info;

        //重新构建item

        BuildAdsItem();
    }

    private void CallBackSaveEditorForSprite(Sprite _sp, AdsStruct info)
    {
        adsStruct[currentIndex] = info;

        itemInfo_AdsItem.SetItemTexture(adsStruct[currentIndex],_sp);
    }

}
