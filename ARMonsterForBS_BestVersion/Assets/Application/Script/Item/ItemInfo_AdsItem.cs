using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ItemInfo_AdsItem : MonoBehaviour {
    public Text textContent;

    public Image imageContent;

    public VideoPlayer videoPlayerContent;

    public GameObject newItemContent;

    private AdsStruct adsStruct;

    public System.Action<AdsStruct> callback_clickItem;

    private Texture2D t2d;

    private byte[] vs;
    public void ClickItem()
    {
        if(callback_clickItem!=null)
        {
            callback_clickItem(adsStruct);
        }
    }

    public void SetInfo(AdsStruct _adsStruct)
    {
        adsStruct = _adsStruct;
        SetItemInfo();
    }
     
    public void SetItemTexture(AdsStruct ast, Sprite _sp)
    {
        adsStruct = ast;

        textContent.gameObject.SetActive(false);
       
        videoPlayerContent.gameObject.SetActive(false);
        videoPlayerContent.Stop();
        newItemContent.gameObject.SetActive(false);
      
        imageContent.gameObject.SetActive(true);
        imageContent.sprite = _sp;
    }

    private void SetItemInfo()
    {
       
        if(t2d!=null)
        {
            vs = null;
            DestroyImmediate(t2d);
            DestroyImmediate(imageContent.sprite);
        }
        textContent.gameObject.SetActive(false);
        imageContent.gameObject.SetActive(false);
        videoPlayerContent.gameObject.SetActive(false);
        videoPlayerContent.Stop();
        newItemContent.gameObject.SetActive(false);
        switch (adsStruct.type)
        {
            case "newItem":
                newItemContent.gameObject.SetActive(true);
                break;
            case "text":
                textContent.gameObject.SetActive(true);
                textContent.text = adsStruct.content;
                break;
            case "texture":
             
                imageContent.gameObject.SetActive(true);

                string s = PlayerPrefs.GetString(AndaDataManager.userAdsKey + adsStruct.content);
                vs = ConvertTool.StringToBytes(s);
                t2d = new Texture2D(GetGameConfigData.adsRule.widthLimit, GetGameConfigData.adsRule.heightLimit);
                t2d.LoadImage(vs);
                imageContent.sprite = Sprite.Create(t2d, new Rect(0,0, t2d.width, t2d.height),
                                                    new Vector2(0.5f, 0.5f));

                AndaUIManager.Instance.OpenWaitBoard(false);
                break;
            case "video":
                videoPlayerContent.gameObject.SetActive(true);
                string path = "";
                if(adsStruct.content.Substring(0,7)!="http://")
                {
                    path = "http://" + adsStruct.content;
                }else
                {
                    path = adsStruct.content;
                }
                videoPlayerContent.url = path;
                videoPlayerContent.Play();
                break;
        }
    }
}
