using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInfo_BusinessCoupon : MonoBehaviour, IPointerClickHandler
{

    public BusinessCouponView CouponView;

    public BussinessRewardStruct coupon;

    public Text titleText;
    public Text statusText;
    public Text descritionText;
    public Text createtimeText;
    public Text dropLimitText;
    public Text dropAmountText;
    public Text dropRateText;
    public Image porImage;

    public Image Bg;

    public GameObject UpperShelf;
    public GameObject LowerShelf;

    public System.Action<BussinessRewardStruct> ClickItem_ToEditor;
    public System.Action<int> ClickItem_UploadtoSell;
    public System.Action<int> ClickItem_DowntoSell;
    public BussinessRewardStruct brs;

    public void SetInfo(BusinessCouponView _CouponView, BussinessRewardStruct _coupon)
    {
        brs = _coupon;
        coupon = _coupon;
        CouponView = _CouponView;
        titleText.text = coupon.title;
        descritionText.text =_coupon.description;
        dropLimitText.text = "总量 :" + _coupon;
        dropAmountText.text = "一次掉落"  + _coupon.rewardDropCount +"张";
        dropRateText.text = "掉落概率 " + _coupon.rewardDropRate +"%";

        AndaDataManager.Instance.GetUserImg(AndaDataManager.Instance.mainData.playerData.headImg,(reuslt=> 
        {
            porImage.sprite = reuslt;
        }));

        LowerShelf.SetActive(false);
        UpperShelf.SetActive(false);
        switch (coupon.status)
        {
            case 0:
                statusText.text = "状态:上架";
                createtimeText.text="创建时间："+ ConvertTool.UnixTimestampToDateTime(coupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
              // Bg.sprite = CouponView.sprite1;
                LowerShelf.SetActive(true);
                break;
            case 1:
                statusText.text = "状态:下架";
                createtimeText.text = "创建时间：" + ConvertTool.UnixTimestampToDateTime(coupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
              //  Bg.sprite = CouponView.sprite2;
                break;
            case 2:
                statusText.text = "状态:作废";
                createtimeText.text = "创建时间：" + ConvertTool.UnixTimestampToDateTime(coupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
              //  Bg.sprite = CouponView.sprite3;
                break;
            case -1:
                statusText.text = "状态:未上架";
                createtimeText.text = "创建时间：" + ConvertTool.UnixTimestampToDateTime(coupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                //Bg.sprite = CouponView.sprite0;
                UpperShelf.SetActive(true);
                break;
        }
    }

   
    public void UpperShelfOnclick()
    {
        AndaUIManager.Instance.PlayTipsForChoose("优惠卷一旦上架无法修改" , 0,"确认" ,"取消" , UpperShelfBack, null);
    }
    public void UpperShelfBack()
    {
        AndaDataManager.Instance.networkController.CallServerUpperShelf(coupon.businesscouponIndex, UpperShelfNetBack);
    }

    public void UpperShelfNetBack(int index)
    {
        if (ClickItem_UploadtoSell != null)
        {
            ClickItem_UploadtoSell(index);
        }
    }

    public void CancelOnclick()
    {
        AndaUIManager.Instance.PlayTipsForChoose("优惠卷一旦上架无法修改", 0, "确认", "取消", CancelBack, null);

    }
    public void CancelBack()
    {
        AndaDataManager.Instance.networkController.CallServerCancel(coupon.businesscouponIndex, CancelNetBack);
    }
    public void CancelNetBack(int couponIndex)
    {
        if(ClickItem_DowntoSell!=null)
        {
            ClickItem_DowntoSell(couponIndex);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (coupon.status == -1)
        {
            CouponView.giftEditionBar.Init(coupon);
            CouponView.giftEditionBar.StartView();
        }
        else {
            AndaUIManager.Instance.PlayTips("只有未上架过的优惠卷才能编辑！");
        }
        //AndaPlayerCouponManager.Instance.selectPlayerCoupon = playerCoupon;
        //AndaPlayerCouponManager.Instance.ShowPlayerCouponDetail(playerCoupon);
    }

    public void ClickItem()
    {
        if(ClickItem_ToEditor!=null)
        {
            ClickItem_ToEditor(brs);
        }
    }
}
