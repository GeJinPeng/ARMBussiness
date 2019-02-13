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
    public Text createtimeText;

    public Image Bg;

    public GameObject UpperShelf;
    public GameObject LowerShelf;

    public void SetInfo(BusinessCouponView _CouponView, BussinessRewardStruct _coupon)
    {
        coupon = _coupon;
        CouponView = _CouponView;
        titleText.text = coupon.title;
        LowerShelf.SetActive(false);
        UpperShelf.SetActive(false);
        switch (coupon.status)
        {
            case 0:
                statusText.text = "状态:上架";
                createtimeText.text="创建时间："+ ConvertTool.UnixTimestampToDateTime(coupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                Bg.sprite = CouponView.sprite1;
                LowerShelf.SetActive(true);
                break;
            case 1:
                statusText.text = "状态:下架";
                createtimeText.text = "创建时间：" + ConvertTool.UnixTimestampToDateTime(coupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                Bg.sprite = CouponView.sprite2;
                break;
            case 2:
                statusText.text = "状态:作废";
                createtimeText.text = "创建时间：" + ConvertTool.UnixTimestampToDateTime(coupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                Bg.sprite = CouponView.sprite3;
                break;
            case -1:
                statusText.text = "状态:未上架";
                createtimeText.text = "创建时间：" + ConvertTool.UnixTimestampToDateTime(coupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                Bg.sprite = CouponView.sprite0;
                UpperShelf.SetActive(true);
                break;
        }
    }

    public void UpperShelfOnclick()
    {
        AndaUIManager.Instance.PlayChoseTips("优惠卷一旦上架无法修改！", UpperShelfBack);
    }
    public void UpperShelfBack()
    {
        AndaDataManager.Instance.networkController.CallServerUpperShelf(coupon.businesscouponIndex, UpperShelfNetBack);
    }
    public void UpperShelfNetBack(BusinessCouponRequest info)
    {
        if (info.code == "200")
            AndaUIManager.Instance.PlayTips("优惠卷已上架！");
        else
            AndaUIManager.Instance.PlayTips("失败！");
    }

    public void CancelOnclick()
    {
        AndaUIManager.Instance.PlayChoseTips("优惠卷一旦作废无法使用！", CancelBack);
    }
    public void CancelBack()
    {
        AndaDataManager.Instance.networkController.CallServerCancel(coupon.businesscouponIndex, CancelNetBack);
    }
    public void CancelNetBack(BusinessCouponRequest info)
    {
        if (info.code == "200")
            AndaUIManager.Instance.PlayTips("优惠卷已作废！");
        else
            AndaUIManager.Instance.PlayTips("失败！");
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (coupon.status == -1)
        {
            CouponView.giftEditionBar.Init(coupon);
            CouponView.giftEditionBar.StartView();
        }
        else {
            AndaUIManager.Instance.PlayTips("只有未上架的优惠卷才能编辑！");
        }
        //AndaPlayerCouponManager.Instance.selectPlayerCoupon = playerCoupon;
        //AndaPlayerCouponManager.Instance.ShowPlayerCouponDetail(playerCoupon);
    }
}
