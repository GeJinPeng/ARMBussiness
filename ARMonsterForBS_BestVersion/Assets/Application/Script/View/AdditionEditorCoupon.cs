using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdditionEditorCoupon : ViewBasic {

    public GridLayoutGroup SelectItemGrid;
    public Animator couponFadeInAnimation;
    public GameObject selectTypeBar;
    public GameObject editorCouponBar;

    public InputField editorNamerInput;
    public InputField editorDescriptionInput;
    public InputField editorDropAmountInput;
    public InputField editorAggregateInput;
    public InputField editorDropmuteInput;
    public Toggle editorCouponchild;

    public Text nameTips;
    public Text descriptionTips;
    public Text dropAmountTips;
    public Text dropLimitTips;
    public Text dropRateTips;

    public Text ComfirName;
    public Text comfirmDes;
    public Text ComfirmDropIntergrate;
    public Text ComfirmDropAmount;
    public Text ComfirmDropRate;




    public Image couponImage;
    public Button photoBtn;

    private bool nameError = false;
    private bool descriptionError = false;
    private bool dropAmountError=false;
    private bool dropIntergrateError=false;
    private bool dropRateError=false;

    private int dropAmountvalue = 1;
    private int dropIntgrateValue = 0;
    private int dropRateValue = 1;

    private string couponNameValue;
    private string couponDesValue;

    private byte[] imageValue;

    private bool isAddtion =false;

    private List<BusinessCoupons> bschildData;

    private bool isSelectTypeStep =false;
    private Vector3 startMousePose;
    private int currentSelectCouponLevel = 0;
    private bool unSave = false;//有变动这个值就要为true
    private bool isFinishUploadCoupon =false;

    private  BussinessRewardStruct bussinessRewardStruct;

    public System.Action<BussinessRewardStruct> callbakc_updateItem;
    public System.Action<BussinessRewardStruct> callback_addtionItem;

    public void EditorCoupon(BussinessRewardStruct brs)
    {
        isAddtion = false;

        bussinessRewardStruct = brs;

        AndaDataManager.Instance.GetUserImg(AndaDataManager.Instance.mainData.playerData.headImg, SetImage);
        isSelectTypeStep = true;

        IsYou(true);

        SelectType("rc_bs_00");

        editorNamerInput.text = brs.title;
        CheckName();

        editorDescriptionInput.text = brs.description;
        CheckDescription();

        editorAggregateInput.text = brs.totalCount.ToString();
        CheckDropIntergrateValue();

        editorDropAmountInput.text = brs.rewardDropCount.ToString();
        CheckDropAmountValue();

        editorDropmuteInput.text = brs.rewardDropRate.ToString();
        CheckDroprate();

    }




   
    public override void StartView()
    {
        isAddtion = true;

        IsYou(true);

        AndaDataManager.Instance.GetUserImg(AndaDataManager.Instance.mainData.playerData.headImg, SetImage);

        //从假的获取 ， 后面还改成玩家的数据
        List<CommodityStructure> commodities = GetGameConfigData.SortRCFormCommodityConfig();
        int count = commodities.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject item = AndaDataManager.Instance.InstantiateItem(AndaDataManager.RCItem00);

            AndaDataManager.Instance.SetInto(item.transform,SelectItemGrid.transform);
            ItemInfo_RCItem00 info_RCItem00 = item.GetComponent<ItemInfo_RCItem00>();
            CommodityStructure objData = GetGameConfigData.GetCommodityStructureItem(commodities[i].commodityID);
            item.name = objData.commodityID;
            info_RCItem00.callback_clickItem = SelectType;
            info_RCItem00.SetInfo(commodities[i].commodityName, AndaDataManager.Instance.GetCommoditySprite(objData.commodityID),AndaDataManager.Instance.CheckItemLessCount(objData.commodityID));
        }
    }
    private void SetImage(Sprite _sp)
    {
        couponImage.sprite = _sp;
    }

    private void SelectType(string id)
    {
        DisplaySelectTypeBar(false);
        DisplayEditorCouponBar(true);

        switch (id)
        {
            case "rc_bs_00":
                currentSelectCouponLevel = 0;
                break;
            case "rc_bs_01":
                currentSelectCouponLevel = 1;
                break;
        }
    }


  


    private void DisplaySelectTypeBar(bool isDisplay )
    {
        isSelectTypeStep = isDisplay;
        selectTypeBar.gameObject.SetActive(isDisplay);
    }

    private void DisplayEditorCouponBar(bool isDisplay)
    {
      
        editorCouponBar.gameObject.SetActive(isDisplay);
    }


    public void Close()
    {
        OutView();
        transform.GetComponent<Animator>().Play("FadeOut");
        Invoke("InvokeDestroy",1f);
    }

    private void InvokeDestroy()
    {
       //AndaUIManager.Instance.uIController.OpenLeftPanel();
        Destroy(gameObject);
    }

  
    private void ClearInputValue()
    {
        editorNamerInput.text = "" ;
        editorDescriptionInput.text = "";
        editorDropAmountInput.text = "";
        editorAggregateInput.text = "";
        editorDropmuteInput.text ="";
        comfirmDes.text = ComfirName.text = ComfirmDropRate.text = ComfirmDropAmount.text = ComfirmDropIntergrate.text = "----";
        unSave = false;
        isFinishUploadCoupon = false;


    }

    public void ListenInoputchange()
    {
        unSave = true;
    }

   

    public void ComfirmSave()
    {
      
    }

    public void GoheadOut()
    {
        ClearInputValue();
        DisplaySelectTypeBar(true);
        DisplayEditorCouponBar(false);
    }


    public void CheckName()
    {

        if(editorNamerInput.text == "")
        {
            nameTips.gameObject.SetActive(true);
            nameTips.text = "名字不能为空";
            return;
        }

        if(editorNamerInput.text.Length > 8)
        {
            nameTips.gameObject.SetActive(true);
            nameTips.text = "名字不可超过8个字符";
            nameError = true;
        }else
        {
            nameError = false;
            couponNameValue =  editorNamerInput.text;
            ComfirName.text = couponNameValue;
            nameTips.gameObject.SetActive(false);
        }
    }

    public void CheckDescription()
    {

        if(editorDescriptionInput.text == "")
        {
            descriptionTips.gameObject.SetActive(true);
            descriptionTips.text = "描述不能为空";
            return;
        }

        if (editorDescriptionInput.text.Length > 25)
        {
            descriptionTips.gameObject.SetActive(true);
            descriptionTips.text = "描述不可超过25个字符";

            descriptionError = true;
        }
        else
        {

            couponDesValue = editorDescriptionInput.text;
            comfirmDes.text = couponDesValue;
            descriptionTips.gameObject.SetActive(false);
            descriptionError = false;
        }
    }

    public void CheckDropAmountValue()
    {
        if(editorDropAmountInput.text == "")
        {
            dropAmountTips.gameObject.SetActive(true);
            dropAmountvalue = 1;
            dropAmountTips.color = Color.gray;
            editorDropAmountInput.text = "1";
            dropAmountTips.text = "一次掉落数量为:" + dropAmountvalue;
            ComfirmDropAmount.text = dropAmountTips.text;
            return;
        }

        try
        {

            if (editorDropAmountInput.text != "") dropAmountvalue = int.Parse(editorDropAmountInput.text);
            int n = dropAmountvalue;
            int l = dropIntgrateValue;
            if (n == 0 || (n > l && l != 0))
            {
                dropAmountTips.gameObject.SetActive(true);
                dropAmountTips.color = Color.red;
                dropAmountTips.text = "掉落最大值不是无限量的情况下，一次掉落数量不能超过总量";
                dropAmountError = true;
            }
            else
            {
                dropAmountTips.color = Color.gray;
                dropAmountTips.text = "一次掉落数量为:" + dropAmountvalue;
                ComfirmDropAmount.text = dropAmountTips.text;
                dropAmountTips.gameObject.SetActive(true);
                dropAmountError = false;
            }
        }catch(System.Exception ex)
        {
            dropAmountTips.color = Color.red;
            dropAmountTips.gameObject.SetActive(true);
            dropAmountTips.text = "无效输入";
            dropAmountError = true;
        }

    }

    public void CheckDropIntergrateValue()
    {
        if(editorAggregateInput.text == "")
        {

            dropLimitTips.gameObject.SetActive(true);
            dropIntgrateValue = 0;
            editorAggregateInput.text = "0";
            dropLimitTips.color = Color.gray;
            dropLimitTips.text = "总量为 ： 无限量";
            ComfirmDropIntergrate.text = dropLimitTips.text;
            return;
        }

        try
        {

            int n = dropAmountvalue;//一次掉落的
            if (editorAggregateInput.text != "") dropIntgrateValue = int.Parse(editorAggregateInput.text);//总量
            int l = dropIntgrateValue;
            if (l != 0 && l < n)
            {
                dropLimitTips.gameObject.SetActive(true);
                dropLimitTips.color = Color.red;
                dropLimitTips.text = "掉落总量不是无限量的情况下，掉落总量设置不能小于一次掉落量";
                dropIntergrateError = true;
            }
            else
            {
                dropLimitTips.color = Color.gray;
                if (dropIntgrateValue == 0) dropLimitTips.text = "总量为 ： 无限量";
                else dropLimitTips.text = "总量为 ：" + dropIntgrateValue;


                ComfirmDropIntergrate.text = dropLimitTips.text;
                //dropIntgrateValue = l;
                dropLimitTips.gameObject.SetActive(true);
                dropIntergrateError = false;
            }


        }
        catch (System.Exception ex)
        {
            dropLimitTips.gameObject.SetActive(true);
            dropLimitTips.color = Color.red;
            dropLimitTips.text = "无效输入";
            dropIntergrateError = true;
        }


    }

    public void CheckDroprate()
    {
        if(editorDropmuteInput.text == "")
        {
            dropRateTips.gameObject.SetActive(true);
            dropRateValue = 1;
            editorDropmuteInput.text = "1";
            dropRateTips.color = Color.gray;
            dropRateTips.text = "掉落概率为 " + dropRateValue + "%";
            ComfirmDropRate.text = dropRateTips.text;
            return;
        }

        try
        {
            int tmp = int.Parse(editorDropmuteInput.text);
            dropRateValue = editorDropmuteInput.text == "" ? 0 : tmp;
            int n = dropRateValue;

            if (n < 1 || n > 100)
            {
                dropRateTips.gameObject.SetActive(true);
                dropRateTips.color = Color.red;
                dropRateTips.text = "掉落概率只能是（1 -100之间)";
                dropRateError = true;

            }
            else
            {
                dropRateTips.gameObject.SetActive(true);
                dropRateTips.color = Color.gray;
                dropRateTips.text = "掉落概率为 " + dropRateValue + "%";
                ComfirmDropRate.text = dropRateTips.text ;
                dropRateTips.gameObject.SetActive(true);
                dropRateError = false;
            }

        }
        catch(System.Exception ex)
        {
            dropRateTips.gameObject.SetActive(true);
            dropRateTips.color = Color.red;
            dropRateTips.text = "无效输入";
            dropRateError = true;
            return;
        }


       
    }

    public void ChangeSelectAddChildCouponEvent(bool isSelect)
    {
        if(editorCouponchild.isOn && currentSelectCouponLevel == 0)
        {
            editorCouponchild.isOn = false;
            AndaUIManager.Instance.PlayTipsForChoose("复合奖励只有高级奖励券可以使用",1, "明白了", "" , null,null);
        }else
        {

        }
    }



    public void ClickSelectCouponReward()
    {
      //  AndaUIManager.Instance.uIController.OpenImageBar(photoBtn , CallbackSelectPhoto);
    }

    public void CallbackSelectPhoto(Texture2D t2d)
    {
        couponImage.sprite = ConvertTool.ConvertToSpriteWithTexture2d(t2d);
        imageValue = t2d.EncodeToPNG();
    }

    private void UploadCallback(BusinessCouponRequest businessCoupons)
    {
        couponFadeInAnimation.Play("FadeIn");
        isFinishUploadCoupon = true;

        if(callbakc_updateItem!=null) callbakc_updateItem(businessCoupons.data);

        if(callback_addtionItem !=null) callback_addtionItem (businessCoupons.data);
    }



    public void ClickSave()
    {

        if(nameError||descriptionError || dropAmountError || dropIntergrateError || dropRateError)
        {
            return;
        }

        if(bussinessRewardStruct == null) bussinessRewardStruct = new BussinessRewardStruct();
       
        bussinessRewardStruct.title = couponNameValue;
        bussinessRewardStruct.description = couponDesValue;
        bussinessRewardStruct.rewardDropRate = dropRateValue;
        bussinessRewardStruct.rewardDropCount = dropAmountvalue;
        bussinessRewardStruct.totalCount = dropIntgrateValue;
        //bussinessRewardStruct.rewardcomposeID = null;

        if (isAddtion)
        {
            AndaDataManager.Instance.networkController.CallServerUploadReward(bussinessRewardStruct, null, UploadCallback);

        }
        else
        {
            AndaDataManager.Instance.networkController.CallServerEditReward(bussinessRewardStruct, null, UploadCallback);

        }


    }

    public void Update()
    {
        if(isMe)
        {
            if (isSelectTypeStep)
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
                        Close();
                    }
                }
            }
            else
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

                        if (isFinishUploadCoupon)
                        {
                            couponFadeInAnimation.Play("FadeOut");
                            ClearInputValue();
                            return;
                        }

                        if (unSave)
                        {
                            AndaUIManager.Instance.PlayTipsForChoose("您有内容尚为保存,执意退出将不会保存现阶段编辑的内容", 0, "执意退出", "继续编辑", GoheadOut, ComfirmSave);
                            return;
                        }
                        else
                        {

                            GoheadOut();
                        }
                    }
                }
            }
        }

    }

}


