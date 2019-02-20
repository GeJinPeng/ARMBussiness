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
     
    public void SetItemTexture(Sprite _sp)
    {
        textContent.gameObject.SetActive(false);
       
        videoPlayerContent.gameObject.SetActive(false);
        videoPlayerContent.Stop();
        newItemContent.gameObject.SetActive(false);
      
        imageContent.gameObject.SetActive(true);
        imageContent.sprite = _sp;
    }

    private void SetItemInfo()
    {
       
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

                string s = PlayerPrefs.GetString(adsStruct.content);

                byte[] vs = ConvertTool.StringToBytes(s);

                Texture2D texture = new Texture2D(1080, 1920);

                texture.LoadImage(vs);
                texture = ConvertTool.ConvertToTexture2d(texture);

                imageContent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                                                    new Vector2(0.5f, 0.5f));
                /*Debug.Log("Key2" + adsStruct.content) ;
                AndaDataManager.Instance.GetAdsImg(adsStruct.content , (result=>
                {
                    imageContent.sprite = result;
                }));*/
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
