using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdsEditorView_EdiorBar : MonoBehaviour {
    public ToggleGroup toggleGroup;
    public Toggle[]toggles;
    public Animator barAnimator;

    public InputField textInput;
    public InputField videoInput;
    public Image textureInput;

    public GameObject comfirmBtn;
     
    public System.Action<AdsStruct>callbcak_saveFinsh;
    public System.Action<Sprite,AdsStruct> callbcak_saveFinshForSprite;
    private AdsStruct adsStruct;
    private BussinessSHRootConfig bsrc;

    private string lastContent = "";

    private string texftValue;
    private Toggle lastToogle;
    private int currentToggleIndex;
    private string currentType;

    public void OpenEditorBar(AdsStruct _adsStruct , BussinessSHRootConfig _bsrc)
    {
        adsStruct = _adsStruct;
        bsrc = _bsrc;
        barAnimator.Play("FadeIn");

       

        switch (adsStruct.type)
        { 
            case "text":
                toggles[0].isOn = true;
                break;
            case "texture":
                toggles[1].isOn = true;

                break;
            case "video":
                toggles[2].isOn = true;
              
                break;
            case "newItem":
                toggles[0].isOn = true;
                break;
        }
    }

    public void ChangeToggle(Toggle t)
    {
        Debug.Log("isON" + t.isOn);
        if(!t.isOn) return;

        int count = toggles.Length;
        for (int i = 0; i < count; i++)
        {
            if(toggles[i].isOn)
            {
                if(bsrc.adsLevel < i)
                {
                    toggles[i].isOn = false;
                    string adsName = GetGameConfigData.GetAdsLevelBoxItem(i).title;
                    string tips = adsName +"广告 不是适用于当前据点等级，请提升据点点击";
                    AndaUIManager.Instance.PlayTipsForChoose(tips,1,"明白了" ,"",null,null);
                    lastToogle.isOn = true;
                    return;
                }
                currentToggleIndex = i;
                lastToogle = toggles[i];
                SetValue(i);
            }
        }
    }

    private void SetValue(int index)
    {
        textInput.text = "";
        videoInput.text = "";
        textureInput.sprite = null;
        textInput.gameObject.SetActive(false);//.ga
        videoInput.gameObject.SetActive(false);
        textureInput.gameObject.SetActive(false);
        comfirmBtn.gameObject.SetActive(false);
        switch (index)
        {
            case 0:
                textInput.gameObject.SetActive(true);
                SetTextContent();
                break;
            case 1:
                textureInput.gameObject.SetActive(true);
            
                SetTexture();
                break;
            case 2:
                videoInput.gameObject.SetActive(true);
                break;
        }
    }


    public void SetTextContent()
    {
        if (!string.IsNullOrEmpty(adsStruct.content))
        {
            textInput.text = adsStruct.content;
        }

        lastContent = textInput.text;
    }

    public void SetVideoPath()
    {
        if (!string.IsNullOrEmpty(adsStruct.content))
        {
            videoInput.text = adsStruct.content;
        }

        lastContent = videoInput.text;
    }

    public void OpenAlumer()
    {
        SetTexture();
    }

    public void SetTexture()
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
#if UNITY_EDITOR

        StartCoroutine(PCLoadTexture());



#else

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);

            if (path != null)
            {
                // 此Action为选取图片后的回调，返回一个Texture2D 
                // AndaUIManager.Instance.OpenWaitBoard(true);

                Texture2D tt = NativeGallery.LoadImageAtPath(path, -1);
               
                if (tt == null)
                {
                    OutEditorBar();
                }else
                {
                    var rect = new Rect(0, 0, tt.width, tt.height);
                    Texture2D result = new Texture2D(tt.width, tt.height, tt.format, false);
                    RenderTexture renderTexture = RenderTexture.GetTemporary((int)tt.width, (int)tt.height, 32);
                    Graphics.Blit(tt, renderTexture);
                    RenderTexture.active = renderTexture;
                    result.ReadPixels(rect, 0, 0);
                    result.Apply();

                    texftValue = ConvertTool.bytesToString(result.EncodeToPNG());

                    textureInput.sprite = ConvertTool.ConvertToSpriteWithTexture2d(tt);

                    ChangeContent();
                }

                AndaUIManager.Instance.OpenWaitBoard(false);
            }
        }, "选择图片", "image/png", -1);

#endif




    }

    private IEnumerator PCLoadTexture()
    {
        string filePath = "file://" + Application.dataPath + @"/Application/Art/Texture/tmp01.png";

        WWW www  = new WWW(filePath);

        yield return www;

        texftValue = ConvertTool.bytesToString(www.texture.EncodeToPNG());

        Texture2D t2d = new Texture2D(1080,1920);

        t2d.LoadImage(www.texture.EncodeToPNG());

        textureInput.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height),
                                            new Vector2(0.5f, 0.5f));
        AndaUIManager.Instance.OpenWaitBoard(false);

        ChangeContent();

    }

    public void ClickComfirm()
    {
        Upload();
    }

    public void ChangeContent()
    {
        switch(adsStruct.type)
        {
            case "text":
                comfirmBtn.gameObject.SetActive(lastContent!= textInput.text);
                break;
            case "texture":
                comfirmBtn.gameObject.SetActive(true);
                break;
            case "video":
                comfirmBtn.gameObject.SetActive(lastContent != videoInput.text);
                break;
        }
    }

    private void Upload()
    {
        AndaDataManager.Instance.networkController.CallServerUploadAds(1, new AdsStruct
        {
            itemIndex = adsStruct.itemIndex,
            type = GetGameConfigData.GetAdsLevelBoxItem(currentToggleIndex).key,
            content = ConverterContent(),

        }, CallBackUploadFinish);
       
    }

    private string ConverterContent()
    {
        string c = "";
        switch(currentToggleIndex)
        {
            case 0:
                c = textInput.text;
                break;
            case 1:
                c = texftValue;
                break;
            case 2:
                c = videoInput.text;
                break;
        }
        return c;
    }

    private void CallBackUploadFinish(AdsStruct infos)
    {
        if(currentToggleIndex == 1)
        {
            if (callbcak_saveFinshForSprite != null)
            {
                callbcak_saveFinshForSprite(textureInput.sprite, infos);
            }
        }
        else
        {
            if (callbcak_saveFinsh != null)
            {
                callbcak_saveFinsh(infos);
            }
        }
        OutEditorBar();
    }

    private void OutEditorBar()
    {
        toggleGroup.SetAllTogglesOff();


        barAnimator.Play("FadeOut");
    }
}
