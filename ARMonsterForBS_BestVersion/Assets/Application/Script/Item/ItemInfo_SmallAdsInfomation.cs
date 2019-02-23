using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ItemInfo_SmallAdsInfomation : MonoBehaviour {


    public Text adsText;
    public Image adsTexture;
    public VideoPlayer adsVideo;
    public GameObject adAdsContet;

    public Text curIndextips;

    private bool loopRun =false;
    private int currentItemIndex;
    private List<AdsStruct> infos;

    private Texture2D t2d;
    private byte[] vs;
    private string t2dValue;

    private BussinessSHRootConfig bussinessSHRootConfig;

    public  System.Action<List<AdsStruct> ,BussinessSHRootConfig> callbackClickToEditorAds;


    private List<string> types = new List<string>
    {
        "text" , "textrue" ,"video" , "newItem"
    };

    public void SetInfo(BusinessStrongholdAttribute data)
    {
        data.strongholdLevel = 4;//测试用
           bussinessSHRootConfig = GetGameConfigData.GetBussinessSHRootConfigItem(data.strongholdLevel);
        ResetAdsSturct(data.adsInfos);
        SetAdsNewItem();

        if (infos == null || infos.Count==0)
        {
            loopRun = false;
        }
        else
        {
            loopRun = true;
            StartCoroutine(LoopRead());
        }

    }

    private void ResetAdsSturct(List<AdsStruct> data)
    {
        infos = new List<AdsStruct>();
        if (data != null && data.Count != 0)
        {
            int count = data.Count;
            for (int i = 0; i < count; i++)
            {
                AdsStruct ad = new AdsStruct();
                ad.itemIndex = data[i].itemIndex;
                ad.content = data[i].content;
                ad.type = data[i].type;
                infos.Add(ad);
            }

        }

    }

    /// <summary>
    /// 补齐空缺
    /// </summary>
    private void SetAdsNewItem()
    {
        int count = bussinessSHRootConfig.adsCount;
        for (int i = 0; i < count; i++)
        {
            if (i >= infos.Count)
            {
                AdsStruct ad = new AdsStruct();
                ad.itemIndex = i;
                ad.content = "";
                ad.type = "newItem";
                infos.Add(ad);
            }
        }
    }




    private IEnumerator LoopRead()
    {
        while(loopRun)
        {
            SetItem();
            float waitTime = 0.5f;
            switch (infos[currentItemIndex].type)
            {
                case "text":
                    waitTime = 2f;
                    break;
                case "texture":
                    waitTime = 5f;
                    break;
                case "video":
                   // adsVideo.source
                    waitTime = 5f;//(float)adsVideo.get.length;
                    break;
                case "newItem":
                    waitTime = 2f;
                    break;
            }
            yield return new WaitForSeconds(waitTime);
            if (currentItemIndex+1 > infos.Count-1)
            {
                currentItemIndex = 0;
                continue;
            }
            currentItemIndex++;
        }
    }

    private void SetItem()
    {
        curIndextips.text = (currentItemIndex+1)+ "/" + infos.Count;

        adsText.gameObject.SetActive(false);
        adsTexture.gameObject.SetActive(false);
        adsVideo.Stop();
        adsVideo.gameObject.SetActive(false);
        adAdsContet.gameObject.SetActive(false);
        switch (infos[currentItemIndex].type)
        {
            case "text":
                SetText();
                break;
            case "texture":
                SetTexture();
                break;
            case "video":
                SetVideo();
                break;
            case "newItem":
                SetNewItem();
                break;
        }
    }

    private void SetNewItem()
    {
        adAdsContet.gameObject.SetActive(true);
    }

    private void SetText()
    {
        adsText.gameObject.SetActive(true);
        if (adsText.text == "")
        {
            adsText.text = infos[currentItemIndex].content;
        }
    }

    private void SetTexture()
    {
        adsTexture.gameObject.SetActive(true);
        if (adsTexture.sprite == null)
        {
            t2dValue = PlayerPrefs.GetString(AndaDataManager.userAdsKey + infos[currentItemIndex].content);
            vs = ConvertTool.StringToBytes(t2dValue);
            t2d = new Texture2D(GetGameConfigData.adsRule.widthLimit, GetGameConfigData.adsRule.heightLimit);
            t2d.LoadImage(vs);
            adsTexture.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height),
                                                new Vector2(0.5f, 0.5f));
        }
    }

    private void SetVideo()
    {
        adsVideo.gameObject.SetActive(true);
        
        if (string.IsNullOrEmpty(adsVideo.url))
        {

          /* string path = "";
            if (infos[currentItemIndex].content.Substring(0, 7) != "http://")
            {
                path = "http://" + infos[currentItemIndex].content;
            }
            else
            {
                path = infos[currentItemIndex].content;
            }
*/
            adsVideo.url = infos[currentItemIndex].content;
        }

        adsVideo.Play();
    }
     
    private void Out()
    {
        if(t2d!=null)
        {
            DestroyImmediate(t2d);
            vs = null;
            t2dValue = null;
            adsTexture.sprite = null;
        }
    }


    private void OnDestroy()
    {
        Out();
    }


    public void ClickItem()
    {
        loopRun =false;
        if (callbackClickToEditorAds!=null)
        {
            callbackClickToEditorAds(infos,bussinessSHRootConfig);
        }
    }



}

