using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInfo_PlayerCoupon : MonoBehaviour , IPointerClickHandler
{


    public PlayerCouponView playerCouponView;

    public PlayerCoupon playerCoupon;

    public Text titleText;
    public Text playernameText;
    public Text statusText;
    public Text applytimeText;
    public Text operationtimeText;

    public Image Bg;
    public void SetInfo(PlayerCouponView _playerCouponView, PlayerCoupon _playerCoupon)
    {
        playerCoupon = _playerCoupon;
        playerCouponView = _playerCouponView;

        titleText.text = playerCoupon.coupon.title;
        playernameText.text ="游戏昵称"+ playerCoupon.playerName;
        if (playerCoupon.status == 0)
        {

        }
        switch (playerCoupon.status)
        {
            case 0:
                statusText.text = "状态:未提交";
                applytimeText.text = "";
                operationtimeText.text = "";
                Bg.sprite = playerCouponView.sprite0;
                break;
            case 1:
                statusText.text = "状态:未审核";
                applytimeText.text ="玩家申请时间:"+ ConvertTool.UnixTimestampToDateTime(playerCoupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                operationtimeText.text = "";
                Bg.sprite = playerCouponView.sprite1;
                break;
            case 2:
                statusText.text = "状态:审核通过";
                applytimeText.text = "玩家申请时间:" + ConvertTool.UnixTimestampToDateTime(playerCoupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                operationtimeText.text = "商家回复时间:" + ConvertTool.UnixTimestampToDateTime(playerCoupon.operationTime).ToString("yyyy-MM-dd HH:mm:ss");
                Bg.sprite = playerCouponView.sprite2;
                break;
            case 3:
                statusText.text = "状态:审核不通过";
                applytimeText.text = "玩家申请时间:" + ConvertTool.UnixTimestampToDateTime(playerCoupon.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                operationtimeText.text = "商家回复时间:" + ConvertTool.UnixTimestampToDateTime(playerCoupon.operationTime).ToString("yyyy-MM-dd HH:mm:ss");
                Bg.sprite = playerCouponView.sprite3;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AndaPlayerCouponManager.Instance.selectPlayerCoupon = playerCoupon;
        AndaPlayerCouponManager.Instance.ShowPlayerCouponDetail(playerCoupon);
    }

   
}
