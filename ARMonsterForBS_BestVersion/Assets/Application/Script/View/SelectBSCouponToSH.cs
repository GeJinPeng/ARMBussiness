using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectBSCouponToSH : ViewBasic {


    public Transform mainCardPoint;
    public Transform grid;
    public Text tips;
    public Text selectTips;
    public Text centerTips;
    private BusinessStrongholdAttribute bsa;
    private ItemInfo_SmallBSCoupon main_Couppon;
    private List<ItemInfo_SmallBSCoupon> freeCouponList;
    private bool unSave = false;
    public System.Action callback_updateBSCoupon;

    public void SetInfo(BusinessStrongholdAttribute _bsa, BussinessRewardStruct data)
    {
        IsYou(true);
        bsa =_bsa;
        if (data == null)
        {
            tips.gameObject.SetActive(true);
            centerTips.gameObject.SetActive(false);
        }
        else
        {
            tips.gameObject.SetActive(false);
            centerTips.gameObject.SetActive(true);

            main_Couppon = BuildItem(data, mainCardPoint);
        }

        BuildCouponList();
    }


    private void BuildCouponList()
    {
        List<BussinessRewardStruct> list = AndaDataManager.Instance.mainData.GetCanusingRewardData();
        if(list == null)
        {
            selectTips.text ="您当前没有可以使用的优惠券，请前往编辑中心编辑";
            return;
        }

        selectTips.text = "从下方优惠券列表选取您想要替换的优惠券";
        int count = list.Count;
        for(int i = 0; i < count; i++)
        {
            ItemInfo_SmallBSCoupon item = BuildItem(list[i],grid);
            item.itemIndex = i;
            item.callback_selectItem = SelectItem;
            if(freeCouponList == null) freeCouponList = new List<ItemInfo_SmallBSCoupon>();
            freeCouponList.Add(item);
        }
    }

    private void SelectItem(BussinessRewardStruct data, int itemIndex)
    {
        if(main_Couppon!=null && main_Couppon.data.businesscouponIndex == data.businesscouponIndex)return;
      
        unSave =true;
        AndaDataManager.Instance.CallServerInsertStrongholdReward(bsa.strongholdIndex, data.businesscouponIndex, (result, result2) =>
        {
            if (main_Couppon != null)
            {
                Destroy(main_Couppon.gameObject);
            }
            main_Couppon = BuildItem(data, mainCardPoint);
            unSave = false;
            tips.gameObject.SetActive(false);
            if(callback_updateBSCoupon!=null)
            {
                callback_updateBSCoupon();
            }
        });

    }



    private ItemInfo_SmallBSCoupon BuildItem(BussinessRewardStruct data, Transform parentsPoint)
    {
        GameObject item = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ItemInfo_SmallBusinessCoupon);
        AndaDataManager.Instance.SetInto(item.transform, parentsPoint);
        ItemInfo_SmallBSCoupon t = item.GetComponent<ItemInfo_SmallBSCoupon>();
        t.SetInfo(data);
        return t;
    }


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
                        if (unSave)
                        {
                            isMe = false;
                            AndaUIManager.Instance.PlayTips("信息尚未保存完!");
                            return;
                        }
                        lastView.IsYou(true);

                        Destroy(gameObject);
                    }
                }
            }
        }
    }

}
