using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessCouponView : MonoBehaviour {

    public GameObject main;


    public GameObject ScrollContent;
    public List<GameObject> ItemList;
    public int Type = 0; // -1全部 0未使用1已提交2已审核成功3已审核审核失败4已过期5已作废

    public Dropdown dropdown;

    public int selectTypeIndex = 0;//当前面板显示类别

    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Animator animator;

    public GiftEditionBar giftEditionBar;

    public void ShowMain()
    {
        SetContentPanel();
        if (main.activeSelf)
            CloseMain();
        else
        {
            gameObject.SetActive(true);
            main.SetActive(true);
            animator.Play("OpenMenu");
        }
    }

    public void CloseMain()
    {
        animator.Play("CloseMenu");
    }
    public void SetActiveTrue()
    {
        main.SetActive(true);
    }
    public void SetActiveFalse()
    {
        main.SetActive(false);
    }
   
    public void SetContentPanel()
    {
        if (ItemList == null)
            ItemList = new List<GameObject>();
        if (AndaDataManager.Instance.mainData.bussinessReward == null)
            return;
        int count = AndaDataManager.Instance.mainData.bussinessReward.Count;
        if (ItemList.Count>0)
        {
            //foreach (var m in ItemList)
            //{
            //    Destroy(m);
            //}
            //ItemList.Clear();
            return;
        }
        for (int i = 0; i < count; i++)
        {
            GameObject item = Instantiate(AndaDataManager.Instance.GetItemInfoPrefab("BussinessCouponItem"));
            item.transform.parent = ScrollContent.transform;
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<ItemInfo_BusinessCoupon>().SetInfo(this, AndaDataManager.Instance.mainData.bussinessReward[i]);
            ItemList.Add(item);
        }
    }

    public void RefreshAddContentPanel(BussinessRewardStruct _coupon)
    {
        GameObject item = Instantiate(AndaDataManager.Instance.GetItemInfoPrefab("BussinessCouponItem"));
        item.transform.parent = ScrollContent.transform;
        item.transform.localScale = Vector3.one;
        item.transform.localPosition = Vector3.zero;
        item.GetComponent<ItemInfo_BusinessCoupon>().SetInfo(this, _coupon);
        ItemList.Add(item);
    }

    public void RefreshEditContentPanel(BussinessRewardStruct _coupon)
    {
        if (ItemList != null)
        {
            foreach (var m in ItemList)
            {
                if (m.GetComponent<ItemInfo_BusinessCoupon>().coupon.businesscouponIndex == _coupon.businesscouponIndex)
                {
                    var info = m.GetComponent<ItemInfo_BusinessCoupon>().coupon;
                    info.title = _coupon.title;
                    info.businessname = _coupon.businessname;
                    info.changeCount = _coupon.changeCount;
                    info.code = _coupon.code;
                    info.continueTime = _coupon.continueTime;
                    info.createTime = _coupon.createTime;
                    info.description = _coupon.description;
                    info.endtime = _coupon.endtime;
                    info.failCount = _coupon.failCount;
                    info.fallenCount = _coupon.fallenCount;
                    info.image = _coupon.image;
                    info.porIsUpdate = _coupon.porIsUpdate;
                    info.rewardcomposeID = _coupon.rewardcomposeID;
                    info.rewardDropCount = _coupon.rewardDropCount;
                    info.rewardDropRate = _coupon.rewardDropRate;
                    info.starttime = _coupon.starttime;
                    info.status = _coupon.status;
                    info.strongholdIndex = _coupon.strongholdIndex;
                    info.tips = _coupon.tips;
                    info.type = _coupon.type;
                    info.totalCount = _coupon.totalCount;
                    info.userIndex = _coupon.userIndex;

                    m.GetComponent<ItemInfo_BusinessCoupon>().SetInfo(this, info);
                }
            }
        }
    }
    public void Cheack()
    {
        AndaPlayerCouponManager.Instance.Cheack();
    }
    public void Fail()
    {
        AndaPlayerCouponManager.Instance.Fail();
    }
    public void ChangeType(int type)
    {
        if (ItemList == null)
            return;
        foreach (var m in ItemList)
        {
            if (m.GetComponent<ItemInfo_BusinessCoupon>().coupon.status == type)
            {
                m.SetActive(true);
            }
            else
            {
                m.SetActive(false);
            }
        }
    }


    public void RightSelect()
    {
        selectTypeIndex--;
        if (selectTypeIndex < 0)
            selectTypeIndex = 2;
        dropdown.value = selectTypeIndex;
    }
    public void LeftSelect()
    {
        selectTypeIndex++;
        if (selectTypeIndex > 2)
            selectTypeIndex = 0;
        dropdown.value = selectTypeIndex;
    }

    public void DropSelect()
    {
        dropdown.OnPointerClick(null);
    }
}
