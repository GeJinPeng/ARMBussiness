using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo_StrongholdInformation : ViewBasic {

    public Transform grid;
    public Text sh_nickName;
    public Text sh_hotValue;
    public Text sh_description;
    public Image sh_por;
  
    private ItemInfo_SmallAdsInfomation itemInfo_SmallAds;
    private ItemInfo_MonsterCar info_MonsterCar;
    private ItemInfo_SmallBSCoupon itemInfo_SmallBSCoupon;
    private BusinessStrongholdAttribute bsa;

    public void OnEnable()
    {
        isMe = true;

    }

    private void UpdateStronghold()
    {
        bsa = AndaDataManager.Instance.mainData.GetAtrongholdAttributes(bsa.strongholdIndex);
    }

    public void SetInfo(BusinessStrongholdAttribute info)
    {
        bsa = info;
        BuildAdsItem();
        BuildMonsterItem();
        BuildCouponItem();
        SetNickName(info.strongholdNickName);
        SetDescription(info.description);
        SetHotValue();
        SetPor();
    }

    private void BuildAdsItem()
    {
        if(itemInfo_SmallAds!=null)Destroy(itemInfo_SmallAds.gameObject);
        GameObject item = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ItemInfo_SmallAdsInfomation);
        AndaDataManager.Instance.SetInto(item.transform, grid.transform);
        itemInfo_SmallAds = item.GetComponent<ItemInfo_SmallAdsInfomation>();
        itemInfo_SmallAds.SetInfo(bsa);
        itemInfo_SmallAds.callbackClickToEditorAds = CallBackOpenSHAdsEditorView;

    }

    private void BuildMonsterItem()
    {
        if(info_MonsterCar!=null )Destroy(info_MonsterCar.gameObject);
        GameObject item = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ItemInfo_SmallMonsterCard);
        AndaDataManager.Instance.SetInto(item.transform, grid.transform);
        info_MonsterCar = item.GetComponent<ItemInfo_MonsterCar>();
        info_MonsterCar.SetInfo(bsa.monsterCardID);
        info_MonsterCar.callbcak_clickItem = CallBackOpenMonsterCardEditorView;
    }

    private void BuildCouponItem()
    {
        if(itemInfo_SmallBSCoupon!=null)Destroy(itemInfo_SmallBSCoupon.gameObject);
        GameObject item = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ItemInfo_SmallBusinessCoupon);
        AndaDataManager.Instance.SetInto(item.transform, grid.transform);
        itemInfo_SmallBSCoupon = item.GetComponent<ItemInfo_SmallBSCoupon>();
        if(bsa.coupons != null && bsa.coupons.Count != 0)
        {
            itemInfo_SmallBSCoupon.SetInfo(AndaDataManager.Instance.mainData.GetRewardData(bsa.coupons[0]));


        }else
        {
            itemInfo_SmallBSCoupon.SetInfo(null);

        }
        itemInfo_SmallBSCoupon.callback_clickItem = CallBackOpendBusnessCouponSelectEditorView;
    }

    private void SetNickName(string nickname)
    {
        sh_nickName.text = nickname;
    }

    private void SetDescription(string des)
    {
        sh_description.text = des;
    }

    private void SetPor()
    {
        sh_por.sprite = AndaDataManager.Instance.mainData.userImage;
    } 

    public void SetHotValue()
    {
        sh_hotValue.text = bsa.strongholdGloryValue.ToString();
    }

    /// <summary>
    /// 修改据点名字
    /// </summary>
    public void ClickOpenEditorStrongholdNameBar()
    {
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ItemEditor_SHBaseInfo);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        ItemEditor_SHNameBar ish = view.GetComponent<ItemEditor_SHNameBar>();
        ish.type = "nickName";
        ish.SetInfo(bsa.strongholdIndex);
        ish.callbcak = SetNickName;
        ish.lastView = this;
        IsYou(false);

    }
    /// <summary>
    /// 修改据点描述
    /// </summary>
    public void ClickOpenEditorStrongholdDescriptionBar()
    {
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ItemEditor_SHBaseInfo);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        ItemEditor_SHNameBar ish = view.GetComponent<ItemEditor_SHNameBar>();
        ish.type = "description";
        ish.SetInfo(bsa.strongholdIndex);
        ish.callbcak = SetDescription;
        ish.lastView = this;
        IsYou(false);
    }
    /// <summary>
    /// 打开广告编辑
    /// </summary>
    /// <param name="infos">Infos.</param>
    /// <param name="bussinessSHRootConfig">Bussiness SHR oot config.</param>
    private void CallBackOpenSHAdsEditorView(List<AdsStruct> infos,BussinessSHRootConfig bussinessSHRootConfig)
    {
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.AdsEditorView);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        AdsEditorVew adsEditorVew = view.GetComponent<AdsEditorVew>();
        adsEditorVew.SetInfo(bsa,infos, bussinessSHRootConfig);
        IsYou(false);
        adsEditorVew.lastView = this;
        adsEditorVew.callback_updateAds = UpdateAds;
    }
    /// <summary>
    /// 打开怪兽卡编辑
    /// </summary>
    private void CallBackOpenMonsterCardEditorView()
    {
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.SelectMonsterCardToSH);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        SelectMonsterCardToSH selectMonsterCardToSH = view.GetComponent<SelectMonsterCardToSH>();
        selectMonsterCardToSH.SetInfo(bsa);
        IsYou(false);
        selectMonsterCardToSH.lastView= this;
        selectMonsterCardToSH.callback_UpdateMonsterCard = UpdateMonsterCard;
    }
    /// <summary>
    /// 打开优惠券编辑
    /// </summary>
    private void CallBackOpendBusnessCouponSelectEditorView()
    {
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.SelectBsCouponToSH);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        SelectBSCouponToSH selectBSCouponToSH = view.GetComponent<SelectBSCouponToSH>();
        selectBSCouponToSH.SetInfo(bsa, itemInfo_SmallBSCoupon.data);
        IsYou(false);
        selectBSCouponToSH.lastView = this;
        selectBSCouponToSH.callback_updateBSCoupon = UpdateBussinessCoupon;
    }


    #region 回调更新界面

    public void UpdateAds()
    {
        UpdateStronghold(); 
        BuildAdsItem();
        ChangeItemIndex(0);
    }

    public void UpdateMonsterCard()
    {
        UpdateStronghold();
        BuildMonsterItem();
        ChangeItemIndex(1);
    }

    public void UpdateBussinessCoupon()
    {
        UpdateStronghold();
        BuildCouponItem();
        ChangeItemIndex(2);
    }

    private void ChangeItemIndex(int i)
    {
        grid.GetChild(grid.childCount -1).SetSiblingIndex(i);
    }

    #endregion


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
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void GiveUpEditor()
    {
        Destroy(gameObject);
    }


    public void TestInserMonsterTOSH()
    {
        AndaDataManager.Instance.networkController.CallServerSetMonsterCard(bsa.strongholdIndex,AndaDataManager.Instance.mainData.monsterCard[1].commodityID, Result);
    }

    public void TestUploadStronghold()
    {
        AndaDataManager.Instance.networkController.CallServerUplevelStronghold(bsa.strongholdIndex, Result2);
    }
    private void Result(int index, string id)
    {
        Debug.Log("sh" +index +":id" +id);
    }
    public void Result2(int index)
    {
        Debug.Log("sh" + index + ":id" + index);
    }


}
