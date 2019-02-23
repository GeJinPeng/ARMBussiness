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
    public Text tipsContent;
    public GameObject tipsItem;
    public System.Action<AdsStruct>callbcak_saveFinsh;
  // public System.Action<Sprite,AdsStruct> callbcak_saveFinshForSprite;
    private AdsStruct adsStruct;
    private BussinessSHRootConfig bsrc;

    private string lastContent = "";

    private string texftValue;
    private Toggle lastToogle;
    private int currentToggleIndex;
    private string currentType;
    private Sprite resultSprite;
    private Texture2D result;
    private int bsaIndex;
    public void OpenEditorBar(int _bsaIndex, AdsStruct _adsStruct , BussinessSHRootConfig _bsrc)
    {
        bsaIndex = _bsaIndex;
        adsStruct = _adsStruct;
        bsrc = _bsrc;
        barAnimator.Play("FadeIn");

       

        switch (adsStruct.type)
        { 
            case "text":
                toggles[0].isOn = true;
                break;
            case "texture":
                //adsRule = ;
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


        if (result != null)
        {
            DestroyImmediate(result);

            DestroyImmediate(resultSprite);// = null;

            textureInput.sprite = null;

            texftValue = null;

        }




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
            if (path != null)
            {
                Texture2D tt = NativeGallery.LoadImageAtPath(path, -1);

                if (tt == null)
                {
                    OutEditorBar();
                }
                else
                {

                    if (tt.width > GetGameConfigData.adsRule.widthLimit || tt.height > GetGameConfigData.adsRule.heightLimit)
                    {
                        AndaUIManager.Instance.PlayTipsForChoose("静态广告的尺寸必须为 256 * 454 像素，请遵循个上传规则", 1, "好", "", null, null);
                        DestroyImmediate(tt);
                        AndaUIManager.Instance.OpenWaitBoard(false);
                        return;
                    }

                    result = new Texture2D(GetGameConfigData.adsRule.widthLimit, GetGameConfigData.adsRule.heightLimit, tt.format, false);
                    RenderTexture renderTexture = RenderTexture.GetTemporary(result.width, result.height, 32);
                    Graphics.Blit(tt, renderTexture);
                    RenderTexture.active = renderTexture;
                    result.ReadPixels(new Rect(0, 0, result.width, result.height), 0, 0);
                    result.Apply();

                    texftValue = ConvertTool.bytesToString(result.EncodeToPNG());

                    textureInput.sprite = Sprite.Create(result, new Rect(0, 0, result.width, result.height), new Vector2(0.5f, 0.5f));

                    ChangeContent();

                    DestroyImmediate(tt);
                }

                AndaUIManager.Instance.OpenWaitBoard(false);
            }
        }, "选择图片", "image/png", -1);
         
#endif



    }

    private IEnumerator PCLoadTexture()
    {
        string filePath = "file://" + Application.dataPath + @"/Application/Art/Texture/ads4.png";

        WWW www  = new WWW(filePath);

        yield return www;

        if(www.texture.width > GetGameConfigData.adsRule.widthLimit || www.texture.height> GetGameConfigData.adsRule.heightLimit)
        {

            AndaUIManager.Instance.PlayTipsForChoose("静态广告的尺寸必须为 256 * 454 像素，请遵循个上传规则",1,"好" ,"",null,null);
            DestroyImmediate(www.texture);
            AndaUIManager.Instance.OpenWaitBoard(false);
            yield break;
        }

        result = new Texture2D(GetGameConfigData.adsRule.widthLimit, GetGameConfigData.adsRule.heightLimit);

        byte[] bt = www.texture.EncodeToPNG();

        result.LoadImage(www.texture.EncodeToPNG());

        texftValue = ConvertTool.bytesToString(result.EncodeToPNG());
       
        resultSprite = Sprite.Create(result, new Rect(0, 0, result.width, result.height),
                                     new Vector2(0.5f, 0.5f));
        textureInput.sprite = resultSprite;

        AndaUIManager.Instance.OpenWaitBoard(false);


        DestroyImmediate(www.texture);

        ChangeContent();

    }

    public void ClickComfirm()
    {
        Upload();
    }

    public void ChangeContent()
    {
        switch(currentToggleIndex)
        {
            case 0:
                comfirmBtn.gameObject.SetActive(lastContent!= textInput.text);
                break;
            case 1:
                comfirmBtn.gameObject.SetActive(true);
                break;
            case 2:
                comfirmBtn.gameObject.SetActive(lastContent != videoInput.text);
                break;
        }
    }

    private void Upload()
    {
        AndaDataManager.Instance.networkController.CallServerUploadAds(bsaIndex, new AdsStruct
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
        if (callbcak_saveFinsh != null)
        {
            callbcak_saveFinsh(infos);
        }
        if(result!=null)
        {
            DestroyImmediate(result);

            DestroyImmediate(resultSprite);// = null;

            textureInput.sprite = null;

            texftValue = null;

        }

        OutEditorBar();
    }

    private void OutEditorBar()
    {
        toggleGroup.SetAllTogglesOff();
        barAnimator.Play("FadeOut");
    }


    public void OpenTips(string tips)
    {
        tipsItem.gameObject.SetActive(true);
        tipsContent.text = tips;
    }

    public void CloseTips()
    {
        tipsItem.gameObject.SetActive(false);
    }

    private Vector3 startMousePose;
    public void FixedUpdate()
    {
        if (gameObject.activeSelf)
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
                    CallBackUploadFinish(null);
                }
            }
        }
    }
}
