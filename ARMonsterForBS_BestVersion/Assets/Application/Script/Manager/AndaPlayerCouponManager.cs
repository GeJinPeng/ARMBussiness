using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LitJson;

public class AndaPlayerCouponManager  {

    private static AndaPlayerCouponManager _instance = null;
    public static AndaPlayerCouponManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AndaPlayerCouponManager();
            }
            return _instance;
        }
    }

    public PlayerCouponView playerCouponView;

    public List<PlayerCoupon> PlayerCouponData;

    public PlayerCoupon selectPlayerCoupon;

    public PlayerCouponDetail playerCouponDetail;


    public void SetPlayerCoupon()
    {
        AndaDataManager.Instance.GetPlayerCoupon(GetPlayerCouponBack);
        AndaMessageManager.Instance.CallBackUpdateCoupon = CallBackUpdateCoupon;
    }

    public void CallBackUpdateCoupon(PlayerCoupon info)
    {
        foreach (var m in playerCouponView.ItemList)
        {
            if (m.GetComponent<ItemInfo_PlayerCoupon>().playerCoupon.applyIndex == info.applyIndex)
            {
                m.GetComponent<ItemInfo_PlayerCoupon>().statusText.text = "状态:未审核";
                m.GetComponent<ItemInfo_PlayerCoupon>().applytimeText.text = "玩家申请时间:" + ConvertTool.UnixTimestampToDateTime(info.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                m.GetComponent<ItemInfo_PlayerCoupon>().operationtimeText.text = "";
                m.GetComponent<ItemInfo_PlayerCoupon>().Bg.sprite = playerCouponView.sprite1;
            }
        }
    }
    public void GetPlayerCouponBack(PlayerCouponsRequest res)
    {
        Debug.Log("获取优惠卷列表");
        if (res.code == "200")
        {
            //playerCouponView.SetContentPanel(res.data);
            PlayerCouponData = res.data;
            //消息列表更新
            if (PlayerCouponData !=null)
            {
                playerCouponView.SetContentPanel(PlayerCouponData);
                playerCouponView.ChangeType(0);
            }
        }
    }

    public List<PlayerCoupon> GetPlayerCouponData(int type=-1)
    {
        if (PlayerCouponData == null)
        {
            SetPlayerCoupon();
            //var josn = PlayerPrefs.GetString("PlayerCoupon");
            //if (josn == "")
            //    PlayerCouponData = new List<PlayerCoupon>();
            //PlayerCouponData = LitJson.JsonMapper.ToObject<List<PlayerCoupon>>(josn);
        }
        if (type >= 0)
            return PlayerCouponData.Where(o => o.status == type).ToList();
        else
            return PlayerCouponData;
    }

    public void Cheack()
    {
        if (selectPlayerCoupon !=null)
            AndaDataManager.Instance.CheackPlayerCoupon(selectPlayerCoupon.applyIndex, CheackBack);
    }

    public void Cheack(int applyIndex)
    {
        AndaDataManager.Instance.CheackPlayerCoupon(applyIndex, CheackBack);
    }
   
    public void CheackBack(PlayerCouponRequest playerCoupon)
    {
        if (playerCoupon.code == "200")
        {
            var list = AndaPlayerCouponManager.Instance.GetPlayerCouponData();
            if (list == null)
                return;

            var item =list.FirstOrDefault(o => o.applyIndex == playerCoupon.data.applyIndex);
            if (item == null)
                return;
            item.status = 2;

            if (playerCouponView == null)
                return;
            foreach (var m in playerCouponView.ItemList)
            {
                if (m.GetComponent<ItemInfo_PlayerCoupon>().playerCoupon.applyIndex == item.applyIndex)
                {
                    m.GetComponent<ItemInfo_PlayerCoupon>().statusText.text = "状态:审核通过";
                    m.GetComponent<ItemInfo_PlayerCoupon>().applytimeText.text = "玩家申请时间:" + ConvertTool.UnixTimestampToDateTime(item.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                    m.GetComponent<ItemInfo_PlayerCoupon>().operationtimeText.text = "商家回复时间:" + ConvertTool.UnixTimestampToDateTime(item.operationTime).ToString("yyyy-MM-dd HH:mm:ss");
                    m.GetComponent<ItemInfo_PlayerCoupon>().Bg.sprite = playerCouponView.sprite2;
                    m.SetActive(false);
                }
            }
            if (playerCouponDetail != null)
                playerCouponDetail.ShowButtonText(2);
        }
    }

    public void Fail()
    {
        if (selectPlayerCoupon != null)
            AndaDataManager.Instance.FailPlayerCoupon(selectPlayerCoupon.applyIndex, FailBack);
    }

    public void Fail(int applyIndex)
    {
        if (selectPlayerCoupon != null)
            AndaDataManager.Instance.FailPlayerCoupon(applyIndex, FailBack);
    }
    public void FailBack(PlayerCouponRequest playerCoupon)
    {
        if (playerCoupon.code == "200")
        {
            var list = AndaPlayerCouponManager.Instance.GetPlayerCouponData();
            if (list == null)
                return;

            var item = list.FirstOrDefault(o => o.applyIndex == playerCoupon.data.applyIndex);
            if (item == null)
                return;
            item.status = 3;

            if (playerCouponView == null)
                return;
            foreach (var m in playerCouponView.ItemList)
            {
                if (m.GetComponent<ItemInfo_PlayerCoupon>().playerCoupon.applyIndex == item.applyIndex)
                {
                    m.GetComponent<ItemInfo_PlayerCoupon>().statusText.text = "状态:审核不通过";
                    m.GetComponent<ItemInfo_PlayerCoupon>().applytimeText.text = "玩家申请时间:" + ConvertTool.UnixTimestampToDateTime(item.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                    m.GetComponent<ItemInfo_PlayerCoupon>().operationtimeText.text = "商家回复时间:" + ConvertTool.UnixTimestampToDateTime(item.operationTime).ToString("yyyy-MM-dd HH:mm:ss");
                    m.GetComponent<ItemInfo_PlayerCoupon>().Bg.sprite = playerCouponView.sprite3;
                    m.SetActive(false);
                }
            }
            if (playerCouponDetail != null)
                playerCouponDetail.ShowButtonText(3);
        }
    }

    public void ShowPlayerCouponDetail(PlayerCoupon _selectPlayerCoupon)
    {
        selectPlayerCoupon = _selectPlayerCoupon;
        if (playerCouponDetail == null)
        {
            GameObject item = AndaDataManager.Instance.GetItemInfoPrefab("PlayerCouponDetail");
            item = Object.Instantiate(item);
            playerCouponDetail = item.GetComponent<PlayerCouponDetail>();
            item.transform.parent=AndaUIManager.Instance.uIController.transform;
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
        }
        playerCouponDetail.Open(selectPlayerCoupon);
    }
}
